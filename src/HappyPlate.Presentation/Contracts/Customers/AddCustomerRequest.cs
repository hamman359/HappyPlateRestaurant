namespace HappyPlate.Presentation.Contracts.Customers;

public sealed record AddCustomerRequest(
    string firstName,
    string lastName,
    string email,
    string areaCode,
    string prefix,
    string lineNumber,
    string? extension,
    List<AddressRequest> addresses);
