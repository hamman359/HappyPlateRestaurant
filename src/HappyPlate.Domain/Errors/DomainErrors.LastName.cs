using HappyPlate.Domain.Shared;

namespace HappyPlate.Domain.Errors;

public static partial class DomainErrors
{
    public static class LastName
    {
        public static readonly Error Empty = new(
            "LastName.Empty",
            "Last name is empty");

        public static readonly Error TooLong = new(
            "LastName.TooLong",
            "Last name is too long");
    }
}