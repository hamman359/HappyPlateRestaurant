namespace HappyPlate.Domain.DomainEvents;

public record MenuItemDeletedDomainEvent(Guid Id) : DomainEvent(Id);
