using HappyPlate.Domain.Errors;
using HappyPlate.Domain.Primatives;
using HappyPlate.Domain.Shared;

namespace HappyPlate.Domain.ValueObjects;

public sealed class MenuItemName : ValueObject
{
    public const int MaxLength = 50;

    MenuItemName(string value) => Value = value;

    public string Value { get; init; }

    public static Result<MenuItemName> Create(string menuItemName)
    {
        if(String.IsNullOrEmpty(menuItemName))
        {
            return Result.Failure<MenuItemName>(DomainErrors.MenuItemName.Empty);
        }

        if(menuItemName.Length > MaxLength)
        {
            return Result.Failure<MenuItemName>(DomainErrors.MenuItemName.TooLong);
        }

        return new MenuItemName(menuItemName);
    }

    public override IEnumerable<object> GetAtomicValues()
    {
        yield return Value;
    }
}
