using HappyPlate.Domain.Shared;

namespace HappyPlate.Domain.Errors;

public static partial class DomainErrors
{
    public static class ZipCode
    {
        public static readonly Error Invalid = new(
            "ZipCode.Invalid",
            "Zip Code is not valid");
    }
}