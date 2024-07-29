using HappyPlate.Application.Abstractions.Messaging;

namespace HappyPlate.Application.MenuItems.Commands.AddMenuItem;

public sealed record AddMenuItemCommand(
    string Name,
    string Description,
    float Price,
    string Category,
    string Image,
    bool IsAvailable) : ICommand<Guid>;