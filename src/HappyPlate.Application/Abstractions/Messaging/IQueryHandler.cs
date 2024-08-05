using HappyPlate.Domain.Shared;

using MediatR;

namespace HappyPlate.Application.Abstractions.Messaging;

/// <summary>
/// Definition for classes that handle Queries in the CQRS implementation.
/// Wraps MediatR's IRequestHandler and ensures the response is a Result Object.
/// Has Type constrain to ensure that TQuery is an IQuery<TResponse>
/// </summary>
/// <typeparam name="TQuery">The Query to handle</typeparam>
/// <typeparam name="TResponse">The response type from the Query</typeparam>
public interface IQueryHandler<TQuery, TResponse>
    : IRequestHandler<TQuery, Result<TResponse>>
    where TQuery : IQuery<TResponse>
{
}