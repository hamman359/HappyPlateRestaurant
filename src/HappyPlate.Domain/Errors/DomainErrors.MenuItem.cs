using HappyPlate.Domain.Shared;

namespace HappyPlate.Domain.Errors;

public static partial class DomainErrors
{
    public static class MenuItem
    {
        public static readonly Func<Guid, NotFoundError> NotFound = id => new NotFoundError(
            "MenuItem.NotFound",
            $"The Menu Item with Id {id} was not found");
    }
}