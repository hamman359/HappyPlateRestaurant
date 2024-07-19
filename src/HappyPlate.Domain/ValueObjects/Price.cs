using HappyPlate.Domain.Errors;
using HappyPlate.Domain.Primatives;
using HappyPlate.Domain.Shared;

namespace HappyPlate.Domain.ValueObjects;

public class Price : ValueObject
{
    Price(float amount) => Amount = amount;

    Price()
    {
    }

    public float Amount { get; private set; }

    public static Result<Price> Create(float amount)
    {
        if(amount < 0.0f)
        {
            return Result.Failure<Price>(DomainErrors.Price.Negative);
        }

        return new Price(amount);
    }

    public override IEnumerable<object> GetAtomicValues()
    {
        yield return Amount;
    }
}
