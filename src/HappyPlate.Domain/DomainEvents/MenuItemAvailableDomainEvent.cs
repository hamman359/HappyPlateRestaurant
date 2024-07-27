namespace HappyPlate.Domain.DomainEvents;

public sealed record MenuItemAvailableDomainEvent(Guid Id) : DomainEvent(Id);