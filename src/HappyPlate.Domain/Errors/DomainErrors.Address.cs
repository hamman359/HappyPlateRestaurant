using HappyPlate.Domain.Shared;

namespace HappyPlate.Domain.Errors;

public static partial class DomainErrors
{
    public static class Address
    {
        public static readonly Error AddressTypeInvalid = new(
            "Address.AddressTypeInvalid",
            "Address Type is not a valid Address Type");

        public static readonly Error StateIsInvalid = new(
            "Address.StateInvalid",
            "State is not valid");
    }
}