namespace HappyPlate.Application.Abstractions.Messaging;

/// <summary>
/// Wrapper interface that adds data necessary for caching to Queries.
/// Adds additional fields, CacheKey and Expiration, that are required for caching to Query objects.
/// </summary>
/// <typeparam name="TResponse">Type returned by the Query</typeparam>
public interface ICachedQuery<TResponse> : IQuery<TResponse>, ICachedQuery;

public interface ICachedQuery
{
    string CacheKey { get; }

    TimeSpan? Expiration { get; }
}
