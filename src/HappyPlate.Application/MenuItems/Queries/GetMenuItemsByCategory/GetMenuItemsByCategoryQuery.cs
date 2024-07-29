using HappyPlate.Application.Abstractions.Messaging;

namespace HappyPlate.Application.MenuItems.Queries.GetMenuItemsByCategory;

public sealed record GetMenuItemsByCategoryQuery(string Category)
    : ICachedQuery<IList<MenuItemResponse>>
{
    public string CacheKey => $"menu-items-by-category-{Category}";

    public TimeSpan? Expiration => null;
}
