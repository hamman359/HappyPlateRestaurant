using HappyPlate.Application.Abstractions.Messaging;
using HappyPlate.Domain.Errors;
using HappyPlate.Domain.Repositories;
using HappyPlate.Domain.Shared;

namespace HappyPlate.Application.MenuItems.ToggleMenuItemAvailabilityCommand;

public sealed class ToggleMenuItemAvailabilityCommandHandler
    : ICommandHandler<ToggleMenuItemAvailabilityCommand, bool>
{
    readonly IMenuItemRepository _menuItemRepository;
    readonly IUnitOfWork _unitOfWork;

    public ToggleMenuItemAvailabilityCommandHandler(
        IMenuItemRepository menuItemRepository,
        IUnitOfWork unitOfWork)
    {
        _menuItemRepository = menuItemRepository;
        _unitOfWork = unitOfWork;
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

        return true;
    }
}
