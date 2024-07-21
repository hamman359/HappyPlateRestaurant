using HappyPlate.Domain.Entities;

namespace HappyPlate.Domain.Repositories;
public interface IMenuItemRepository
{
    Task<MenuItem?> GetByIdAsync(Guid productId, CancellationToken cancellationToken);
    void Add(MenuItem product);
}
