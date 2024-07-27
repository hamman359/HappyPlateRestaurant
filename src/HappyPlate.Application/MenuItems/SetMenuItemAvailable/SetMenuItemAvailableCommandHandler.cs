using HappyPlate.Application.Abstractions.Messaging;
using HappyPlate.Domain.DomainEvents;
using HappyPlate.Domain.Errors;
using HappyPlate.Domain.Repositories;
using HappyPlate.Domain.Shared;

using MediatR;

namespace HappyPlate.Application.MenuItems.SetMenuItemAvailable;

public sealed class SetMenuItemAvailableCommandHandler
    : ICommandHandler<SetMenuItemAvailableCommand, bool>
{
    readonly IMenuItemRepository _menuItemRepository;
    readonly IUnitOfWork _unitOfWork;
    readonly IPublisher _publisher;

    public SetMenuItemAvailableCommandHandler(
        IMenuItemRepository menuItemRepository,
        IUnitOfWork unitOfWork,
        IPublisher publisher)
    {
        _menuItemRepository = menuItemRepository;
        _unitOfWork = unitOfWork;
        _publisher = publisher;
    }

    public async Task<Result<bool>> Handle(
        SetMenuItemAvailableCommand request,
        CancellationToken cancellationToken)
    {
        var menuItem = await _menuItemRepository.GetByIdAsync(request.MenuItemId, cancellationToken);

        if(menuItem is null)
        {
            return Result.Failure<bool>(DomainErrors.MenuItem.NotFound(request.MenuItemId));
        }

        menuItem.SetAsAvailable();

        await _unitOfWork.SaveChangesAsync(cancellationToken);

        var menuItemUnavailableEvent = new MenuItemAvailableDomainEvent(menuItem.Id);

        await _publisher.Publish(menuItemUnavailableEvent, cancellationToken);

        return true;
    }
}
