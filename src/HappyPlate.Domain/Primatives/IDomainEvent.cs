using MediatR;

namespace HappyPlate.Domain.Primatives;

public interface IDomainEvent : INotification
{
    public Guid Id { get; init; }
}