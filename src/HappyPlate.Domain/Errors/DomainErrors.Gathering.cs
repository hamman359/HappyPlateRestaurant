using HappyPlate.Domain.Shared;

namespace HappyPlate.Domain.Errors;

public static partial class DomainErrors
{
    public static class Gathering
    {
        public static readonly Func<Guid, Error> NotFound = id => new Error(
            "Gathering.NotFound",
            $"The gathering with the identifier {id} was not found.");

        public static readonly Error InvitingCreator = new(
            "Gathering.InvitingCreator",
            "Can't send invitation to the gathering creator");

        public static readonly Error AlreadyPassed = new(
            "Gathering.AlreadyPassed",
            "Can't send invitation for gathering in the past");

        public static readonly Error Expired = new(
            "Gathering.Expired",
            "Can't accept invitation for expired gathering");
    }
}