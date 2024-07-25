using HappyPlate.Application.Abstractions.Messaging;

namespace HappyPlate.Application.MenuItems.DeleteMenuItem;

public sealed record DeleteMenuItemCommand(Guid MenuItemId) : ICommand<bool>;
