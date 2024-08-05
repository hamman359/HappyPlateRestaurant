using HappyPlate.Application.Abstractions.Messaging;
using HappyPlate.Application.Caching;
using HappyPlate.Domain.Shared;

using MediatR;

namespace HappyPlate.Application.Behaviors;

/// <summary>
/// Defines a MediatR pipeline behavior for handling the Caching of Queries
/// Has Type Constraints to ensure TRequest is an ICachedQuery and that TResponse is a Result.
/// </summary>
public sealed class QueryCachingPipelineBehavior<TRequest, TResponse>
    : IPipelineBehavior<TRequest, TResponse>
    where TRequest : ICachedQuery
    where TResponse : Result
{
    readonly ICacheService _cacheService;

    public QueryCachingPipelineBehavior(ICacheService cacheService)
    {
        _cacheService = cacheService;
    }

    /// <summary>
    /// Checks if the requested data is already cached.
    /// If so, then returns the cached version.
    /// Otherwise, adds the data to the cache.
    /// </summary>
    public async Task<TResponse> Handle(
        TRequest request,
        RequestHandlerDelegate<TResponse> next,
        CancellationToken cancellationToken)
    {
        return await _cacheService.GetOrCreateAsync(
            request.CacheKey,
            _ => next(),
            request.Expiration,
            cancellationToken);
    }
}
