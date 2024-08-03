namespace HappyPlate.Domain.DomainEvents;

public record CustomerCreatedDomainEvent(
    Guid Id,
    IEnumerable<Guid> AddressIds) : DomainEvent(Id);
