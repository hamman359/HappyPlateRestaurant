using HappyPlate.Application.Abstractions.Messaging;
using HappyPlate.Domain.Entities;
using HappyPlate.Domain.Repositories;
using HappyPlate.Domain.Shared;
using HappyPlate.Domain.ValueObjects;

namespace HappyPlate.Application.Products.AddProduct;

public class AddProductCommandHandler : ICommandHandler<AddProductCommand, Guid>
{
    readonly IProductRepository _productRepository;
    readonly IUnitOfWork _unitOfWork;

    public AddProductCommandHandler(
        IProductRepository productRepository,
        IUnitOfWork unitOfWork)
    {
        _productRepository = productRepository;
        _unitOfWork = unitOfWork;
    }

    public async Task<Result<Guid>> Handle(
        AddProductCommand request,
        CancellationToken cancellationToken)
    {
        Result<Price> priceResult = Price.Create(request.Price);

        if(priceResult.IsFailure)
        {
            return Result.Failure<Guid>(priceResult.Error);
        }

        var product = Product.Create(
            request.Name,
            request.Description,
            priceResult.Value,
            request.Category);

        _productRepository.Add(product);

        await _unitOfWork.SaveChangesAsync(cancellationToken);

        return product.Id;
    }
}
