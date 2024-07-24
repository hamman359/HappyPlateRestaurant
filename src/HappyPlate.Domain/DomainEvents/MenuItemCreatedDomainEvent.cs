namespace HappyPlate.Domain.DomainEvents;

public record MenuItemCreatedDomainEvent(
    Guid Id,
    string Name,
    string Description,
    string Category,
    float Price,
    string Image,
    bool isAvailable) : DomainEvent(Id);
