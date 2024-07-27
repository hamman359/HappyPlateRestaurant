using HappyPlate.Application.Abstractions.Messaging;
using HappyPlate.Domain.DomainEvents;
using HappyPlate.Domain.Entities;
using HappyPlate.Domain.Repositories;
using HappyPlate.Domain.Shared;
using HappyPlate.Domain.ValueObjects;

using MediatR;

namespace HappyPlate.Application.MenuItems.AddMenuItem;

public sealed class AddMenuItemCommandHandler : ICommandHandler<AddMenuItemCommand, Guid>
{
    readonly IMenuItemRepository _menuItemRepository;
    readonly IUnitOfWork _unitOfWork;
    readonly IPublisher _publisher;

    public AddMenuItemCommandHandler(
        IMenuItemRepository menuItemRepository,
        IUnitOfWork unitOfWork,
        IPublisher publisher)
    {
        _menuItemRepository = menuItemRepository;
        _unitOfWork = unitOfWork;
        _publisher = publisher;
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

        Result<MenuItemName> menuItemName = MenuItemName.Create(request.Name);

        if(menuItemName.IsFailure)
        {
            return Result.Failure<Guid>(menuItemName.Error);
        }

        var menuItem = MenuItem.Create(
            menuItemName.Value,
            request.Description,
            priceResult.Value,
            request.Category,
            request.Image,
            request.IsAvailable);

        _menuItemRepository.Add(menuItem);

        await _unitOfWork.SaveChangesAsync(cancellationToken);

        var menuItemCreatedEvent = new MenuItemCreatedDomainEvent(
            menuItem.Id,
            menuItem.Name.Value,
            menuItem.Description,
            menuItem.Category,
            menuItem.Price.Amount,
            menuItem.Image,
            menuItem.IsAvailable);

        await _publisher.Publish(menuItemCreatedEvent, cancellationToken);

        return menuItem.Id;
    }
}
