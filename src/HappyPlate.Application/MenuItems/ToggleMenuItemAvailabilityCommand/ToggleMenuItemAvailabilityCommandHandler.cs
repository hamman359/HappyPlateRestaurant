using HappyPlate.Application.Abstractions.Messaging;
using HappyPlate.Domain.DomainEvents;
using HappyPlate.Domain.Errors;
using HappyPlate.Domain.Repositories;
using HappyPlate.Domain.Shared;

using MediatR;

namespace HappyPlate.Application.MenuItems.ToggleMenuItemAvailabilityCommand;

public sealed class ToggleMenuItemAvailabilityCommandHandler
    : ICommandHandler<ToggleMenuItemAvailabilityCommand, bool>
{
    readonly IMenuItemRepository _menuItemRepository;
    readonly IUnitOfWork _unitOfWork;
    readonly IPublisher _publisher;

    public ToggleMenuItemAvailabilityCommandHandler(
        IMenuItemRepository menuItemRepository,
        IUnitOfWork unitOfWork,
        IPublisher publisher)
    {
        _menuItemRepository = menuItemRepository;
        _unitOfWork = unitOfWork;
        _publisher = publisher;
    }

    public async Task<Result<bool>> Handle(
        ToggleMenuItemAvailabilityCommand request,
        CancellationToken cancellationToken)
    {
        var menuItem = await _menuItemRepository.GetByIdAsync(request.MenuItemId, cancellationToken);

        if(menuItem is null)
        {
            return Result.Failure<bool>(DomainErrors.MenuItem.NotFound(request.MenuItemId));
        }

        menuItem.ToggleAvailability();

        await _unitOfWork.SaveChangesAsync(cancellationToken);

        var productUpdatedEvent = new MenuItemUpdatedDomainEvent(
            menuItem.Id,
            menuItem.Name.Value,
            menuItem.Description,
            menuItem.Category,
            menuItem.Price.Amount,
            menuItem.Image,
            menuItem.IsAvailable);

        await _publisher.Publish(productUpdatedEvent, cancellationToken);

        return true;
    }
}
