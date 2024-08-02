using HappyPlate.Domain.Entities;

namespace HappyPlate.Domain.Repositories;

public interface ICustomerRepository
{
    void Add(Customer customer);
    Task<Customer?> GetByIdAsync(Guid customerId, CancellationToken cancellationToken);
}
