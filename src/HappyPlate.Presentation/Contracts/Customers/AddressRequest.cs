namespace HappyPlate.Presentation.Contracts.Customers;

public sealed record AddressRequest(
    string street,
    string city,
    string state,
    string zipCode,
    string country,
    string addressType);
