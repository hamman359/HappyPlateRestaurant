using HappyPlate.Application.Abstractions.Messaging;

namespace HappyPlate.Application.Products.AddProduct;

public sealed record AddProductCommand(
    string Name,
    string Description,
    float Price,
    string Category) : ICommand<Guid>;
