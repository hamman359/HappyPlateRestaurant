using HappyPlate.Application.Abstractions.Messaging;

namespace HappyPlate.Application.MenuItems.ToggleMenuItemAvailability;

public sealed record ToggleMenuItemAvailabilityCommand(Guid MenuItemId) : ICommand<bool>;
