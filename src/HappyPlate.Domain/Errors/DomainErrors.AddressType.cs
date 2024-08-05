using HappyPlate.Domain.Shared;

namespace HappyPlate.Domain.Errors;

public static partial class DomainErrors
{
    public static class AddressType
    {
        public static readonly Error Invalid = new(
            "AddressType.Invalid",
            "AddressType is not a valid value");
    }
}