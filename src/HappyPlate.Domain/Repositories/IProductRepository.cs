using HappyPlate.Domain.Entities;

namespace HappyPlate.Domain.Repositories;
public interface IProductRepository
{
    Task<Product?> GetByIdAsync(Guid productId, CancellationToken cancellationToken);
    void Add(Product product);
}
