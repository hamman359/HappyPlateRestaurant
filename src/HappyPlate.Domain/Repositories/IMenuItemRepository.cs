using HappyPlate.Domain.Entities;

namespace HappyPlate.Domain.Repositories;

public interface IMenuItemRepository
{
    Task<MenuItem?> GetByIdAsync(Guid menuItemId, CancellationToken cancellationToken);
    void Add(MenuItem menuItem);
    void Remove(MenuItem menuItem);
    Task<IList<MenuItem>> GetAllAsync(CancellationToken cancellationToken);
}
