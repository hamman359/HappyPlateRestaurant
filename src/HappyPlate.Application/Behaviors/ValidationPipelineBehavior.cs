using FluentValidation;

using HappyPlate.Domain.Shared;

using MediatR;

namespace HappyPlate.Application.Behaviors;

/// <summary>
/// Defines a MediatR pipeline behavior for performing input validation of requests that come through MediatR.
/// Has Type Constraints to ensure TRequest is an IRequest<> and that TResponse is a Result.
/// </summary>
public class ValidationPipelineBehavior<TRequest, TResponse>
    : IPipelineBehavior<TRequest, TResponse>
    where TRequest : IRequest<TResponse>
    where TResponse : Result
{
    private readonly IEnumerable<IValidator<TRequest>> _validators;

    public ValidationPipelineBehavior(IEnumerable<IValidator<TRequest>> validators) =>
        _validators = validators;

    /// <summary>
    /// Validates the request.
    /// Ifany errors, returns validation result.
    /// Otherwise, returns the result of the next() delegate execution.
    /// Skips the validation if there are not any validators defined.
    /// </summary>
    public async Task<TResponse> Handle(
        TRequest request,
        RequestHandlerDelegate<TResponse> next,
        CancellationToken cancellationToken)
    {
        if(!_validators.Any())
        {
            return await next();
        }

        Error[] errors = _validators
            .Select(validator => validator.Validate(request))
            .SelectMany(validationResult => validationResult.Errors)
            .Where(validationFailure => validationFailure is not null)
            .Select(failure => new Error(
                failure.PropertyName,
                failure.ErrorMessage))
            .Distinct()
            .ToArray();

        if(errors.Any())
        {
            return CreateValidationResult<TResponse>(errors);
        }

        return await next();
    }


    private static TResult CreateValidationResult<TResult>(Error[] errors)
        where TResult : Result
    {
        if(typeof(TResult) == typeof(Result))
        {
            return (ValidationResult.WithErrors(errors) as TResult)!;
        }

        object validationResult = typeof(ValidationResult<>)
            .GetGenericTypeDefinition()
            .MakeGenericType(typeof(TResult).GenericTypeArguments[0])
            .GetMethod(nameof(ValidationResult.WithErrors))!
            .Invoke(null, new object?[] { errors })!;

        return (TResult)validationResult;
    }
}