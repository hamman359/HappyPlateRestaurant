using System.Collections.Immutable;

using HappyPlate.Application.Abstractions.Messaging;
using HappyPlate.Domain.Errors;
using HappyPlate.Domain.Repositories;
using HappyPlate.Domain.Shared;

namespace HappyPlate.Application.Customers.Queries;

public sealed class GetCustomerByIdQueryHandler
    : IQueryHandler<GetCustomerByIdQuery, CustomerResponse>
{
    readonly ICustomerRepository _customerRepository;

    public GetCustomerByIdQueryHandler(ICustomerRepository customerRepository)
    {
        _customerRepository = customerRepository;
    }

    public async Task<Result<CustomerResponse>> Handle(
        GetCustomerByIdQuery request,
        CancellationToken cancellationToken)
    {
        var customer = await _customerRepository.GetByIdAsync(
            request.CustomerId,
            cancellationToken);

        if(customer is null)
        {
            return Result.Failure<CustomerResponse>(DomainErrors.Customer.NotFound(request.CustomerId));
        }

        var response = new CustomerResponse(
            customer.FirstName.Value,
            customer.LastName.Value,
            customer.Email.Value,
            customer.PhoneNumber.Number,
            customer.Addresses
                .Select(a => new AddressResponse(
                    a.Street,
                    a.City,
                    a.State.Name,
                    a.ZipCode.Value,
                    a.Country,
                    a.Type.Name))
                .ToImmutableList());

        return response;
    }
}
