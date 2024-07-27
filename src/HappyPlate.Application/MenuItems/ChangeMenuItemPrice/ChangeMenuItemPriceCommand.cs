using HappyPlate.Application.Abstractions.Messaging;

namespace HappyPlate.Application.MenuItems.ChangeMenuItemPrice;

public sealed record ChangeMenuItemPriceCommand(Guid Id, float Price) : ICommand<Guid>;
