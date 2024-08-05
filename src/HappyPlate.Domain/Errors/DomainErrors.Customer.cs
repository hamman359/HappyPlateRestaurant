using HappyPlate.Domain.Shared;

namespace HappyPlate.Domain.Errors;

public static partial class DomainErrors
{
    public static class Customer
    {
        public static readonly Func<Guid, NotFoundError> NotFound = id => new NotFoundError(
            "Customer.NotFound",
            $"The Customer with Id {id} was not found");
    }
}