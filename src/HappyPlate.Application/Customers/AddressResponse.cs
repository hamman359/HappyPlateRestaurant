namespace HappyPlate.Application.Customers;

public sealed record AddressResponse(
    string Street,
    string City,
    string State,
    string ZipCode,
    string Country,
    string Type);
