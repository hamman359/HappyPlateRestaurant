using HappyPlate.Domain.Shared;

namespace HappyPlate.Domain.Errors;

public static partial class DomainErrors
{
    public static class State
    {
        public static readonly Error Invalid = new(
            "State.Invalid",
            "State is not a valid value");
    }
}