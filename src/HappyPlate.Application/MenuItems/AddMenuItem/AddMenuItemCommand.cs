using HappyPlate.Application.Abstractions.Messaging;

namespace HappyPlate.Application.MenuItems.AddMenuItem;

public sealed record AddMenuItemCommand(
    string Name,
    string Description,
    float Price,
    string Category,
    string Image) : ICommand<Guid>;