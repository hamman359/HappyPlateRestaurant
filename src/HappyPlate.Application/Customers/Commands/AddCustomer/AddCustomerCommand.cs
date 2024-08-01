using HappyPlate.Application.Abstractions.Messaging;

namespace HappyPlate.Application.Customers.Commands.AddCustomer;

public sealed record AddCustomerCommand(
    string FirstName,
    string LastName,
    string Email,
    string AreaCode,
    string Prefix,
    string LineNumber,
    string? Extension,
    IList<AddressDto> Addresses) : ICommand<Guid>;
