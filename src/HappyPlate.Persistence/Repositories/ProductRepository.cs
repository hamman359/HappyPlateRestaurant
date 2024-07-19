using HappyPlate.Domain.Entities;
using HappyPlate.Domain.Repositories;

namespace HappyPlate.Persistence.Repositories;

internal sealed class ProductRepository : IProductRepository
{
    readonly ApplicationDbContext _dbContext;

    public ProductRepository(ApplicationDbContext dbContext)
    {
        _dbContext = dbContext;
    }

    public void Add(Product product) => _dbContext.Set<Product>().Add(product);
}
