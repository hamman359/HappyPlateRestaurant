using HappyPlate.Application.Abstractions.Messaging;

namespace HappyPlate.Application.MenuItems.ToggleMenuItemAvailabilityCommand;

public sealed record ToggleMenuItemAvailabilityCommand(Guid MenuItemId) : ICommand<bool>;
