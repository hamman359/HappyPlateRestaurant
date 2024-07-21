namespace HappyPlate.Presentation.Contracts.MenuItems;

public sealed record AddMenuItemRequest(
    string name,
    string description,
    float price,
    string category,
    string image);
