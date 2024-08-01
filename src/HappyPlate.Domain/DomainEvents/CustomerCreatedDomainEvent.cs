namespace HappyPlate.Domain.DomainEvents;

public record CustomerCreatedDomainEvent(
    Guid Id,
    string FirstName,
    string LastName,
    string Email,
    string PhoneNumber) : DomainEvent(Id);
