using HappyPlate.Domain.Shared;

using MediatR;

namespace HappyPlate.Application.Abstractions.Messaging;

/// <summary>
/// Definition for classes that are Commands and do not return any data
/// </summary>
public interface ICommand : IRequest<Result>
{
}

/// <summary>
/// Definition for classes that are Commands in the CQRS implementation.
/// Wraps MediatR's IRequest and ensures the response is a Result Object.
/// </summary>
/// <typeparam name="TResponse">The Type returned by the command</typeparam>
public interface ICommand<TResponse> : IRequest<Result<TResponse>>
{
}