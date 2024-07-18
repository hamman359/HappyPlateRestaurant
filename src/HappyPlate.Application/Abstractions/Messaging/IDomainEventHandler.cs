using HappyPlate.Domain.Primatives;

using MediatR;

namespace HappyPlate.Application.Abstractions.Messaging;

public interface IDomainEventHandler<TEvent> : INotificationHandler<TEvent>
    where TEvent : IDomainEvent
{
}