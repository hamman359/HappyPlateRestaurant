namespace HappyPlate.Application.Customers.Commands.AddCustomer;

public record AddressDto(
        string Street,
        string City,
        string State,
        string ZipCode,
        string Country,
        string Type);
