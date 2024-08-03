using HappyPlate.Application.Abstractions.Messaging;
using HappyPlate.Domain.DomainEvents;
using HappyPlate.Domain.Errors;
using HappyPlate.Domain.Repositories;
using HappyPlate.Domain.Shared;

using MediatR;

namespace HappyPlate.Application.Customers.Commands.DeleteCustomer;

public sealed class DeleteCustomerCommandHandler
    : ICommandHandler<DeleteCustomerCommand, bool>
{
    readonly ICustomerRepository _customerRepository;
    readonly IUnitOfWork _unitOfWork;
    readonly IPublisher _publisher;

    public DeleteCustomerCommandHandler(
        ICustomerRepository customerRepository,
        IUnitOfWork unitOfWork,
        IPublisher publisher)
    {
        _customerRepository = customerRepository;
        _unitOfWork = unitOfWork;
        _publisher = publisher;
    }

    public async Task<Result<bool>> Handle(
        DeleteCustomerCommand request,
        CancellationToken cancellationToken)
    {
        var customer = await _customerRepository.GetByIdAsync(request.CustomerId, cancellationToken);

        if(customer is null)
        {
            return Result.Failure<bool>(DomainErrors.Customer.NotFound(request.CustomerId));
        }

        _customerRepository.Remove(customer);

        await _unitOfWork.SaveChangesAsync(cancellationToken);

        var customerDeletedEvent = new CustomerDeletedDomainEvent(
            customer.Id,
            customer.FirstName.Value,
            customer.LastName.Value,
            customer.PhoneNumber.Number,
            customer.Email.Value,
            customer.Addresses.Select(x => x.Id).ToList());

        await _publisher.Publish(customerDeletedEvent, cancellationToken);

        return true;
    }

}
