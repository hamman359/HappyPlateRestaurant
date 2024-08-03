using HappyPlate.Domain.Entities;
using HappyPlate.Domain.Repositories;
using HappyPlate.Persistence.Specifications;

using Microsoft.EntityFrameworkCore;

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

    public async Task<Customer?> GetByIdAsync(
        Guid customerId,
        CancellationToken cancellationToken) =>
            await _dbContext
                .Set<Customer>()
                .Include(x => x.Addresses)
                .FirstOrDefaultAsync(x => x.Id == customerId);
    //await ApplySpecification(new CustomerByIdWithAddressesSpecification(customerId))
    //    .FirstOrDefaultAsync();

    IQueryable<Customer> ApplySpecification(
        Specification<Customer> specification)
    {
        return SpecificationEvaluator.GetQuery(
            _dbContext.Set<Customer>(),
            specification);
    }

    public void Remove(Customer customer) =>
        _dbContext
            .Set<Customer>()
            .Remove(customer);
}
