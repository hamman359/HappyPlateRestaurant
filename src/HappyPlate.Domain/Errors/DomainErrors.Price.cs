using HappyPlate.Domain.Shared;

namespace HappyPlate.Domain.Errors;

public static partial class DomainErrors
{
    public static class Price
    {
        public static readonly Error Negative = new(
            "Price.Negative",
            "Price is a negative amount");
    }
}