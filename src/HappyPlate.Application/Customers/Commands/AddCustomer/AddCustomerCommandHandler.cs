using HappyPlate.Application.Abstractions.Messaging;
using HappyPlate.Domain.Entities;
using HappyPlate.Domain.Enums;
using HappyPlate.Domain.Errors;
using HappyPlate.Domain.Repositories;
using HappyPlate.Domain.Shared;
using HappyPlate.Domain.ValueObjects;

namespace HappyPlate.Application.Customers.Commands.AddCustomer;

public sealed class AddCustomerCommandHandler
    : ICommandHandler<AddCustomerCommand, Guid>
{
    readonly ICustomerRepository _customerRepository;
    readonly IUnitOfWork _unitOfWork;

    public AddCustomerCommandHandler(
        ICustomerRepository customerRepository,
        IUnitOfWork unitOfWork)
    {
        _customerRepository = customerRepository;
        _unitOfWork = unitOfWork;
    }

    public async Task<Result<Guid>> Handle(
        AddCustomerCommand request,
        CancellationToken cancellationToken)
    {
        Result<FirstName> firstNameResult = FirstName.Create(request.FirstName);

        if(firstNameResult.IsFailure)
        {
            return Result.Failure<Guid>(firstNameResult.Error);
        }

        Result<LastName> lastNameResult = LastName.Create(request.LastName);

        if(lastNameResult.IsFailure)
        {
            return Result.Failure<Guid>(lastNameResult.Error);
        }

        Result<Email> emailResult = Email.Create(request.Email);

        if(emailResult.IsFailure)
        {
            return Result.Failure<Guid>(emailResult.Error);
        }

        Result<PhoneNumber> phoneNumberResult = PhoneNumber.Create(
            request.AreaCode,
            request.Prefix,
            request.LineNumber,
            request.Extension);

        if(phoneNumberResult.IsFailure)
        {
            return Result.Failure<Guid>(phoneNumberResult.Error);
        }

        List<Address> addresses = new();

        foreach(var address in request.Addresses)
        {
            Result<ZipCode> zipCodeResult = ZipCode.Create(address.ZipCode);

            if(zipCodeResult.IsFailure)
            {
                return Result.Failure<Guid>(DomainErrors.ZipCode.Invalid);
            }

            State? state = State.FromName(address.State);

            if(state is null)
            {
                return Result.Failure<Guid>(DomainErrors.State.Invalid);
            }

            AddressType? addressType = AddressType.FromName(address.Type);

            if(addressType is null)
            {
                return Result.Failure<Guid>(DomainErrors.AddressType.Invalid);
            }

            addresses.Add(
                Address.Create(
                    address.Street,
                    address.City,
                    state,
                    zipCodeResult.Value,
                    address.Country,
                    addressType));
        }

        Customer customer = Customer.Create(
            firstNameResult.Value,
            lastNameResult.Value,
            emailResult.Value,
            phoneNumberResult.Value,
            addresses);

        _customerRepository.Add(customer);

        await _unitOfWork.SaveChangesAsync(cancellationToken);

        return customer.Id;
    }
}
