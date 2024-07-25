namespace HappyPlate.Domain.DomainEvents;

public record MenuItemDeletedDomainEvent(
    Guid Id,
    string Name,
    string Description,
    string Category,
    float Price,
    string Image,
    bool isAvailable) : DomainEvent(Id);
