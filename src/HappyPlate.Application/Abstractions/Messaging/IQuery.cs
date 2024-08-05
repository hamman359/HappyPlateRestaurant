using HappyPlate.Domain.Shared;

using MediatR;

namespace HappyPlate.Application.Abstractions.Messaging;

/// <summary>
/// Definition for classes that are Queries in the CQRS implementation.
/// Wraps MediatR's IRequest and ensures the response is a Result Object.
/// </summary>
/// <typeparam name="TResponse">Type returned by the query</typeparam>
public interface IQuery<TResponse> : IRequest<Result<TResponse>>
{
}