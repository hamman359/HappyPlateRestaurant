using HappyPlate.Application.Abstractions.Messaging;
using HappyPlate.Domain.Errors;
using HappyPlate.Domain.Repositories;
using HappyPlate.Domain.Shared;

namespace HappyPlate.Application.MenuItems.Queries.GetMenuItemById;
public sealed class GetMenuItemByIdQueryHandler
    : IQueryHandler<GetMenuItemByIdQuery, MenuItemResponse>
{
    readonly IMenuItemRepository _menuItemRepository;

    public GetMenuItemByIdQueryHandler(IMenuItemRepository menuItemRepository)
    {
        _menuItemRepository = menuItemRepository;
    }

    public async Task<Result<MenuItemResponse>> Handle(
        GetMenuItemByIdQuery request,
        CancellationToken cancellationToken)
    {
        var menuItem = await _menuItemRepository.GetByIdAsync(
            request.MenuItemId,
            cancellationToken);

        if(menuItem is null)
        {
            return Result.Failure<MenuItemResponse>(DomainErrors.MenuItem.NotFound(request.MenuItemId));
        }

        var response = new MenuItemResponse(
            menuItem.Id,
            menuItem.Name.Value,
            menuItem.Description,
            menuItem.Category,
            menuItem.Price.Amount,
            menuItem.Image,
            menuItem.IsAvailable);

        return response;
    }
}
