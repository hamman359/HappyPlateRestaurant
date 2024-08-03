using HappyPlate.Application.Abstractions.Messaging;

namespace HappyPlate.Application.Customers.Commands.DeleteCustomer;

public sealed record DeleteCustomerCommand(Guid CustomerId) : ICommand<bool>;
