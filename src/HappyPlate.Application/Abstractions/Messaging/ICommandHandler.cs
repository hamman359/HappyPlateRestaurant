using HappyPlate.Domain.Shared;

using MediatR;

namespace HappyPlate.Application.Abstractions.Messaging;

/// <summary>
/// Definition for Command Handlers for Commands that do not return data.
/// </summary>
/// <typeparam name="TCommand">The Command to handle</typeparam>
public interface ICommandHandler<TCommand> : IRequestHandler<TCommand, Result>
    where TCommand : ICommand
{
}

/// <summary>
/// Marker Interface for defining CommandHandler that wraps MediatR's IRequestHandler
/// and wraps the response in a Result Object.
/// Has Type constrain to ensure that TCommand is an ICommand<TResponse>
/// </summary>
/// <typeparam name="TCommand">The Command to handle</typeparam>
/// <typeparam name="TResponse">The response type from the Command</typeparam>
public interface ICommandHandler<TCommand, TResponse>
    : IRequestHandler<TCommand, Result<TResponse>>
    where TCommand : ICommand<TResponse>
{
}