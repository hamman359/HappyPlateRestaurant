using HappyPlate.Application.Abstractions.Messaging;
using HappyPlate.Domain.DomainEvents;
using HappyPlate.Domain.Errors;
using HappyPlate.Domain.Repositories;
using HappyPlate.Domain.Shared;
using HappyPlate.Domain.ValueObjects;

using MediatR;

namespace HappyPlate.Application.MenuItems.Commands.ChangeMenuItemPrice;

public sealed class ChangeMenuItemPriceCommandHandler
    : ICommandHandler<ChangeMenuItemPriceCommand, Guid>
{
    readonly IMenuItemRepository _menuItemRepository;
    readonly IUnitOfWork _unitOfWork;
    readonly IPublisher _publisher;

    public ChangeMenuItemPriceCommandHandler(
        IMenuItemRepository menuItemRepository,
        IUnitOfWork unitOfWork,
        IPublisher publisher)
    {
        _menuItemRepository = menuItemRepository;
        _unitOfWork = unitOfWork;
        _publisher = publisher;
    }



    public async Task<Result<Guid>> Handle(
        ChangeMenuItemPriceCommand request,
        CancellationToken cancellationToken)
    {
        var menuItem = await _menuItemRepository.GetByIdAsync(request.Id, cancellationToken);

        if (menuItem is null)
        {
            return Result.Failure<Guid>(DomainErrors.MenuItem.NotFound(request.Id));
        }

        Result<Price> priceResult = Price.Create(request.Price);

        if (priceResult.IsFailure)
        {
            return Result.Failure<Guid>(priceResult.Error);
        }

        var originalPrice = menuItem.Price.Amount;

        menuItem.ChangePrice(priceResult.Value);

        await _unitOfWork.SaveChangesAsync(cancellationToken);

        await _publisher.Publish(
            new MenuItemPriceChangedDomainEvent(menuItem.Id, originalPrice, menuItem.Price.Amount),
            cancellationToken);

        return menuItem.Id;
    }
}
