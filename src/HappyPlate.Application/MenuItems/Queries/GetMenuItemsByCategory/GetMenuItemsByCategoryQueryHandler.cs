using HappyPlate.Application.Abstractions.Messaging;
using HappyPlate.Domain.Repositories;
using HappyPlate.Domain.Shared;

namespace HappyPlate.Application.MenuItems.Queries.GetMenuItemsByCategory;

public sealed class GetMenuItemsByCategoryQueryHandler
    : IQueryHandler<GetMenuItemsByCategoryQuery, IList<MenuItemResponse>>
{
    readonly IMenuItemRepository _menuItemRepository;

    public GetMenuItemsByCategoryQueryHandler(IMenuItemRepository menuItemRepository)
    {
        _menuItemRepository = menuItemRepository;
    }

    public async Task<Result<IList<MenuItemResponse>>> Handle(
        GetMenuItemsByCategoryQuery request,
        CancellationToken cancellationToken)
    {
        var menuItems = await _menuItemRepository.GetByCategoryAsync(
            request.Category,
            cancellationToken);

        var response = menuItems
            .Select(m => new MenuItemResponse(
                m.Id,
                m.Name.Value,
                m.Description,
                m.Category,
                m.Price.Amount,
                m.Image,
                m.IsAvailable))
            .ToList();

        return response;
    }
}
