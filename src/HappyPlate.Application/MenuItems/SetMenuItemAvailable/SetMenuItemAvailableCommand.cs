using HappyPlate.Application.Abstractions.Messaging;

namespace HappyPlate.Application.MenuItems.SetMenuItemAvailable;

public sealed record SetMenuItemAvailableCommand(Guid MenuItemId) : ICommand<bool>;
