namespace HappyPlate.Domain.DomainEvents;

public record MenuItemUpdatedDomainEvent(
    Guid Id,
    string Name,
    string Description,
    string Category,
    float Price,
    string Image,
    bool isAvailable) : DomainEvent(Id);
