using HappyPlate.Domain.Errors;
using HappyPlate.Domain.Primatives;
using HappyPlate.Domain.Shared;

namespace HappyPlate.Domain.ValueObjects;

public sealed class MenuItemName : ValueObject
{
    MenuItemName(string value) => Value = value;

    public string Value { get; init; }

    public static Result<MenuItemName> Create(string value)
    {
        if(String.IsNullOrEmpty(value))
        {
            return Result.Failure<MenuItemName>(DomainErrors.MenuItemName.Empty);
        }

        return new MenuItemName(value);
    }

    public override IEnumerable<object> GetAtomicValues()
    {
        yield return Value;
    }
}
