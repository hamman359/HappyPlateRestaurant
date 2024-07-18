using HappyPlate.Domain.Primatives;

namespace HappyPlate.Domain.ValueObjects;

public class Price : ValueObject
{
    Price(float amount) => Amount = amount;

    Price()
    {
    }

    public float Amount { get; private set; }

    public static Price Create(float amount)
    {
        return new Price(amount);
    }

    public override IEnumerable<object> GetAtomicValues()
    {
        yield return Amount;
    }
}
