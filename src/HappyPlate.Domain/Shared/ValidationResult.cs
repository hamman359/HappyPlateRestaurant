namespace HappyPlate.Domain.Shared;

/// <summary>
/// Wrapper around Result for Validations
/// </summary>
public sealed class ValidationResult : Result, IValidationResult
{
    private ValidationResult(Error[] errors)
        : base(false, IValidationResult.ValidationError) =>
        Errors = errors;

    public Error[] Errors { get; }

    /// <summary>
    /// Allows for creating a validation result by specifying an array validation errors.
    /// </summary>
    public static ValidationResult WithErrors(Error[] errors) => new(errors);
}