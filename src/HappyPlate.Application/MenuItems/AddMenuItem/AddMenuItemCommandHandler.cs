using HappyPlate.Application.Abstractions.Messaging;
using HappyPlate.Domain.Entities;
using HappyPlate.Domain.Repositories;
using HappyPlate.Domain.Shared;
using HappyPlate.Domain.ValueObjects;

namespace HappyPlate.Application.MenuItems.AddMenuItem;

public class AddMenuItemCommandHandler : ICommandHandler<AddMenuItemCommand, Guid>
{
    readonly IMenuItemRepository _menuItemRepository;
    readonly IUnitOfWork _unitOfWork;

    public AddMenuItemCommandHandler(
        IMenuItemRepository productRepository,
        IUnitOfWork unitOfWork)
    {
        _menuItemRepository = productRepository;
        _unitOfWork = unitOfWork;
    }

    public async Task<Result<Guid>> Handle(
        AddMenuItemCommand request,
        CancellationToken cancellationToken)
    {
        Result<Price> priceResult = Price.Create(request.Price);

        if(priceResult.IsFailure)
        {
            return Result.Failure<Guid>(priceResult.Error);
        }

        var product = MenuItem.Create(
            request.Name,
            request.Description,
            priceResult.Value,
            request.Category,
            request.Image);

        _menuItemRepository.Add(product);

        await _unitOfWork.SaveChangesAsync(cancellationToken);

        return product.Id;
    }
}
