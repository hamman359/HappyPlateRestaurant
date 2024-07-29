using HappyPlate.Application.Abstractions.Messaging;

namespace HappyPlate.Application.MenuItems.Queries.GetMenuItemById;

//public sealed record GetMenuItemByIdQuery(Guid MenuItemId) : IQuery<MenuItemResponse>;
public sealed record GetMenuItemByIdQuery(Guid MenuItemId) : ICachedQuery<MenuItemResponse>
{
    public string CacheKey => $"menu-items-by-id-{MenuItemId}";

    public TimeSpan? Expiration => null;
}