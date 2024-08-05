using HappyPlate.Domain.Shared;

using MediatR;

using Microsoft.Extensions.Logging;

namespace HappyPlate.Application.Behaviors;

/// <summary>
/// Defines a MediatR pipeline behavior for logging requests that come through MediatR.
/// Has Type Constraints to ensure TRequest is an IRequest<> and that TResponse is a Result.
/// </summary>
public class LoggingPipelineBehavior<TRequest, TResponse>
    : IPipelineBehavior<TRequest, TResponse>
    where TRequest : IRequest<TResponse>
    where TResponse : Result
{
    private readonly ILogger<LoggingPipelineBehavior<TRequest, TResponse>> _logger;

    public LoggingPipelineBehavior(ILogger<LoggingPipelineBehavior<TRequest, TResponse>> logger)
    {
        _logger = logger;
    }

    /// <summary>
    /// Logs when a request started and when it finished.
    /// If the request returned a failure response this is also logged.
    /// </summary>
    public async Task<TResponse> Handle(
        TRequest request,
        RequestHandlerDelegate<TResponse> next,
        CancellationToken cancellationToken)
    {
        _logger.LogInformation(
            "Starting request {@RequestName}, {@DateTimeUtc}",
            typeof(TRequest).Name,
            DateTime.UtcNow);

        var result = await next();

        if(result.IsFailure)
        {
            _logger.LogError(
                "Request failure {@RequestName}, {@Error}, {@DateTimeUtc}",
                typeof(TRequest).Name,
                result.Error,
                DateTime.UtcNow);
        }

        _logger.LogInformation(
            "Completed request {@RequestName}, {@DateTimeUtc}",
            typeof(TRequest).Name,
            DateTime.UtcNow);

        return result;
    }
}