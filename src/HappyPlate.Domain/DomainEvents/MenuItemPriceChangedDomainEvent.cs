namespace HappyPlate.Domain.DomainEvents;

public record MenuItemPriceChangedDomainEvent(Guid Id, float originalPrice, float newPrice) : DomainEvent(Id);
