namespace HappyPlate.Domain.DomainEvents;

public sealed record MenuItemUnavailableDomainEvent(Guid Id) : DomainEvent(Id);
