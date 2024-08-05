using HappyPlate.Domain.Shared;

namespace HappyPlate.Domain.Errors;

public static partial class DomainErrors
{
    public static class FirstName
    {
        public static readonly Error Empty = new(
            "FirstName.Empty",
            "First name is empty");

        public static readonly Error TooLong = new(
            "LastName.TooLong",
            "FirstName name is too long");
    }
}