using HappyPlate.Domain.Shared;

namespace HappyPlate.Domain.Errors;

public static partial class DomainErrors
{
    public static class MenuItemName
    {
        public static readonly Error Empty = new(
            "MenuItemName.Empty",
            "Menu Item name is empty");

        public readonly static Error TooLong = new(
            "MenuItemName.TooLong",
            "MenuItem name is too long");

    }
}