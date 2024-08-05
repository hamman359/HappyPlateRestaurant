using HappyPlate.Domain.Primatives;

using MediatR;

namespace HappyPlate.Application.Abstractions.Messaging;

/// <summary>
/// Definition for classes that Handle Domain Events.
/// Wraps MediatR's INotificationHandler.
/// Has Type constrain to ensure that TEvent is an IDomainEvent
/// </summary>
/// <typeparam name="TEvent">The Domain Event to be handled</typeparam>
public interface IDomainEventHandler<TEvent> : INotificationHandler<TEvent>
    where TEvent : IDomainEvent
{
}