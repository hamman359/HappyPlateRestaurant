using HappyPlate.Application.Abstractions.Messaging;
using HappyPlate.Domain.Repositories;
using HappyPlate.Domain.Shared;

namespace HappyPlate.Application.MenuItems.Queries.GetAllMenuItems;
public sealed class GetAllMenuItemsQueryHandler
    : IQueryHandler<GetAllMenuItemsQuery, IList<MenuItemResponse>>
{
    readonly IMenuItemRepository _menuItemRepository;

    public GetAllMenuItemsQueryHandler(IMenuItemRepository menuItemRepository)
    {
        _menuItemRepository = menuItemRepository;
    }

    public async Task<Result<IList<MenuItemResponse>>> Handle(
        GetAllMenuItemsQuery request,
        CancellationToken cancellationToken)
    {
        var menuItems = await _menuItemRepository.GetAllAsync(cancellationToken);

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
