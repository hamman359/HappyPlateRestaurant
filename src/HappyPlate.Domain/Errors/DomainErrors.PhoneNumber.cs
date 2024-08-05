using HappyPlate.Domain.Shared;

namespace HappyPlate.Domain.Errors;

public static partial class DomainErrors
{
    public static class PhoneNumber
    {
        public static readonly Error AreaCodeEmpty = new(
            "PhoneNumber.AreaCodeEmpty",
            "Phone Number Area Code is Empty");

        public static readonly Error PrefixEmpty = new(
            "PhoneNumber.PrefixEmpty",
            "Phone Number Prefix is Empty");

        public static readonly Error LineNumberEmpty = new(
            "PhoneNumber.LineNumberEmpty",
            "Phone Number Line Number is Empty");

        public static readonly Error AreaCodeNotNumber = new(
            "PhoneNumber.AreaCodeNotNumber",
            "Phone Number Area Code is not a valid number");

        public static readonly Error PrefixNotNumber = new(
            "PhoneNumber.PrefixNotNumber",
            "Phone Number Prefix is not a valid number");

        public static readonly Error LineNumberNotNumber = new(
            "PhoneNumber.LineNumberNotNumber",
            "Phone Number Line Number is not a valid number");

        public static readonly Error AreaCodeInvalidLength = new(
            "PhoneNumber.AreaCodeInvalidLength",
            "Phone Number Area Code must be exactly 3 digits long");

        public static readonly Error PrefixInvalidLength = new(
            "PhoneNumber.PrefixInvalidLength",
            "Phone Number Prefix must be exactly 3 digits long");

        public static readonly Error LineNumberInvalidLength = new(
            "PhoneNumber.LineNumberInvalidLength",
            "Phone Number Line Number must be exactly 4 digits long");
    }
}