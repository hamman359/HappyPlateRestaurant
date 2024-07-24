using HappyPlate.Domain.Errors;
using HappyPlate.Domain.Primatives;
using HappyPlate.Domain.Shared;

namespace HappyPlate.Domain.ValueObjects;

public sealed class Price : ValueObject
{
    Price(float amount) => Amount = amount;

    public float Amount { get; init; }

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
