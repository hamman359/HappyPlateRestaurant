using HappyPlate.Application.Abstractions.Messaging;
using HappyPlate.Domain.DomainEvents;
using HappyPlate.Domain.Errors;
using HappyPlate.Domain.Repositories;
using HappyPlate.Domain.Shared;

using MediatR;

namespace HappyPlate.Application.MenuItems.Commands.SetMenuItemUnavailable;

public sealed class SetMenuItemUnavailableCommandHandler
    : ICommandHandler<SetMenuItemUnavailableCommand, bool>
{
    readonly IMenuItemRepository _menuItemRepository;
    readonly IUnitOfWork _unitOfWork;
    readonly IPublisher _publisher;

    public SetMenuItemUnavailableCommandHandler(
        IMenuItemRepository menuItemRepository,
        IUnitOfWork unitOfWork,
        IPublisher publisher)
    {
        _menuItemRepository = menuItemRepository;
        _unitOfWork = unitOfWork;
        _publisher = publisher;
    }

    public async Task<Result<bool>> Handle(
        SetMenuItemUnavailableCommand request,
        CancellationToken cancellationToken)
    {
        var menuItem = await _menuItemRepository.GetByIdAsync(request.MenuItemId, cancellationToken);

        if (menuItem is null)
        {
            return Result.Failure<bool>(DomainErrors.MenuItem.NotFound(request.MenuItemId));
        }

        menuItem.SetAsUnavailable();

        await _unitOfWork.SaveChangesAsync(cancellationToken);

        var menuItemUnavailableEvent = new MenuItemUnavailableDomainEvent(menuItem.Id);

        await _publisher.Publish(menuItemUnavailableEvent, cancellationToken);

        return true;
    }
}
