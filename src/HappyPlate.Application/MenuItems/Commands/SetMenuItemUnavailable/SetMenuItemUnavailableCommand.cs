using HappyPlate.Application.Abstractions.Messaging;

namespace HappyPlate.Application.MenuItems.Commands.SetMenuItemUnavailable;

public sealed record SetMenuItemUnavailableCommand(Guid MenuItemId) : ICommand<bool>;
