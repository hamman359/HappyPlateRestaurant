using HappyPlate.Domain.Entities;
using HappyPlate.Domain.Repositories;

using Microsoft.EntityFrameworkCore;

namespace HappyPlate.Persistence.Repositories;

internal sealed class ProductRepository : IProductRepository
{
    readonly ApplicationDbContext _dbContext;

    public ProductRepository(ApplicationDbContext dbContext)
    {
        _dbContext = dbContext;
    }


    public async Task<Product?> GetByIdAsync(Guid productId, CancellationToken cancellationToken) =>
        await _dbContext
            .Set<Product>()
            .FirstOrDefaultAsync(x => x.Id == productId, cancellationToken);

    public void Add(Product product) => _dbContext.Set<Product>().Add(product);
}
