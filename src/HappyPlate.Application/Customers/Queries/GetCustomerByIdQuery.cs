using HappyPlate.Application.Abstractions.Messaging;

namespace HappyPlate.Application.Customers.Queries;

public sealed record GetCustomerByIdQuery(Guid CustomerId)
    : ICachedQuery<CustomerResponse>
{
    public string CacheKey => $"customer-by-id-{CustomerId}";

    public TimeSpan? Expiration => null;
}
