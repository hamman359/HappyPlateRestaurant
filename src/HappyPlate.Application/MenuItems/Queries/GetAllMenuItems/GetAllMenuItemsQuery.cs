﻿using HappyPlate.Application.Abstractions.Messaging;

namespace HappyPlate.Application.MenuItems.Queries.GetAllMenuItems;
public sealed record GetAllMenuItemsQuery()
    : ICachedQuery<IList<MenuItemResponse>>
{
    public string CacheKey => $"menu-items";

    public TimeSpan? Expiration => null;
}
