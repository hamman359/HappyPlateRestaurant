using HappyPlate.Application.Abstractions.Messaging;
using HappyPlate.Domain.Repositories;
using HappyPlate.Domain.Shared;

namespace HappyPlate.Application.Products.GetProductById;
public sealed class GetProductByIdQueryHandler
    : IQueryHandler<GetProductByIdQuery, ProductResponse>
{
    readonly IProductRepository _productRepository;

    public GetProductByIdQueryHandler(IProductRepository productRepository)
    {
        _productRepository = productRepository;
    }

    public async Task<Result<ProductResponse>> Handle(
        GetProductByIdQuery request,
        CancellationToken cancellationToken)
    {
        var product = await _productRepository.GetByIdAsync(
            request.ProductId,
            cancellationToken);

        if(product is null)
        {
            return Result.Failure<ProductResponse>(new Error(
                "Product.NotFound",
                $"The product with Id {request.ProductId} was not found"));
        }

        var response = new ProductResponse(
            product.Id,
            product.Name,
            product.Description,
            product.Category,
            product.Price.Amount);

        return response;
    }
}
