﻿namespace HappyPlate.Application.MenuItems;


public sealed record MenuItemResponse(
    Guid Id,
    string Name,
    string Description,
    string Category,
    float Price,
    string Image,
    bool IsAvailable);
