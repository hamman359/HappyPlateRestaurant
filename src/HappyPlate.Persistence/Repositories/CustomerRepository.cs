using HappyPlate.Domain.Entities;
using HappyPlate.Domain.Repositories;

namespace HappyPlate.Persistence.Repositories;

public sealed class CustomerRepository
    : ICustomerRepository
{

    readonly ApplicationDbContext _dbContext;

    public CustomerRepository(ApplicationDbContext dbContext)
    {
        _dbContext = dbContext;
    }

    public void Add(Customer customer) =>
        _dbContext
            .Set<Customer>()
            .Add(customer);
}
