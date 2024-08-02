namespace HappyPlate.Application.Customers;

public sealed record CustomerResponse(
        string FirstName,
        string LastName,
        string Email,
        string PhoneNumber,
        IReadOnlyCollection<AddressResponse> Addresses);
