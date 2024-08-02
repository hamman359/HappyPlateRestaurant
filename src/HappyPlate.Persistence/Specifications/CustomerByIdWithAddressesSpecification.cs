using HappyPlate.Domain.Entities;

namespace HappyPlate.Persistence.Specifications;

internal class CustomerByIdWithAddressesSpecification : Specification<Customer>
{
    public CustomerByIdWithAddressesSpecification(Guid customerId)
        : base(customer => customer.Id == customerId)
    {
        AddInclude(customer => customer.Addresses);
    }
}
