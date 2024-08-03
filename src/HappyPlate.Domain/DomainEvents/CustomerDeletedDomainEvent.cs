namespace HappyPlate.Domain.DomainEvents;

public record CustomerDeletedDomainEvent(
    Guid Id,
    string FirstName,
    string LastName,
    string PhoneNumber,
    string Email,
    IList<Guid> AddressIds) : DomainEvent(Id);
