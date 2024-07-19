using HappyPlate.Application.Abstractions.Messaging;

namespace HappyPlate.Application.Products.GetProductById;
public sealed record GetProductByIdQuery(Guid ProductId) : IQuery<ProductResponse>;
