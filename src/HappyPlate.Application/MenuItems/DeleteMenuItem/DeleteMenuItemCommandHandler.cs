using HappyPlate.Application.Abstractions.Messaging;
using HappyPlate.Domain.DomainEvents;
using HappyPlate.Domain.Errors;
using HappyPlate.Domain.Repositories;
using HappyPlate.Domain.Shared;

using MediatR;

namespace HappyPlate.Application.MenuItems.DeleteMenuItem;

public sealed class DeleteMenuItemCommandHandler
    : ICommandHandler<DeleteMenuItemCommand, bool>
{
    readonly IMenuItemRepository _menuItemRepository;
    readonly IUnitOfWork _unitOfWork;
    readonly IPublisher _publisher;

    public DeleteMenuItemCommandHandler(
        IMenuItemRepository menuItemRepository,
        IUnitOfWork unitOfWork,
        IPublisher publisher)
    {
        _menuItemRepository = menuItemRepository;
        _unitOfWork = unitOfWork;
        _publisher = publisher;
    }

    public async Task<Result<bool>> Handle(
        DeleteMenuItemCommand request,
        CancellationToken cancellationToken)
    {
        var menuItem = await _menuItemRepository.GetByIdAsync(request.MenuItemId, cancellationToken);

        if(menuItem is null)
        {
            return Result.Failure<bool>(DomainErrors.MenuItem.NotFound(request.MenuItemId));
        }

        _menuItemRepository.Remove(menuItem);

        await _unitOfWork.SaveChangesAsync(cancellationToken);

        var menuItemDeletedEvent = new MenuItemDeletedDomainEvent(
            menuItem.Id,
            menuItem.Name.Value,
            menuItem.Description,
            menuItem.Category,
            menuItem.Price.Amount,
            menuItem.Image,
            menuItem.IsAvailable);

        await _publisher.Publish(menuItemDeletedEvent, cancellationToken);

        return true;
    }
}
