using HappyPlate.Domain.Entities;
using HappyPlate.Domain.Repositories;

using Microsoft.EntityFrameworkCore;

namespace HappyPlate.Persistence.Repositories;

internal sealed class MenuItemRepository : IMenuItemRepository
{
    readonly ApplicationDbContext _dbContext;

    public MenuItemRepository(ApplicationDbContext dbContext)
    {
        _dbContext = dbContext;
    }

    public async Task<MenuItem?> GetByIdAsync(Guid menuItemId, CancellationToken cancellationToken) =>
        await _dbContext
            .Set<MenuItem>()
            .FirstOrDefaultAsync(x => x.Id == menuItemId, cancellationToken);

    public void Add(MenuItem menuItem) =>
        _dbContext
            .Set<MenuItem>()
            .Add(menuItem);

    public void Remove(MenuItem menuItem) =>
        _dbContext
            .Set<MenuItem>()
            .Remove(menuItem);

    public async Task<IList<MenuItem>> GetAllAsync(CancellationToken cancellationToken) =>
        await _dbContext
            .Set<MenuItem>()
            .ToListAsync(cancellationToken);
}
