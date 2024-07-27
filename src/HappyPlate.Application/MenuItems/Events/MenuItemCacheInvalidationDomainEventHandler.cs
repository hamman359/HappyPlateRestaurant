using HappyPlate.Application.Abstractions.Messaging;
using HappyPlate.Application.Caching;
using HappyPlate.Domain.DomainEvents;

namespace HappyPlate.Application.MenuItems.Events;

internal class MenuItemCacheInvalidationDomainEventHandler
    : IDomainEventHandler<MenuItemPriceChangedDomainEvent>
    , IDomainEventHandler<MenuItemDeletedDomainEvent>
    , IDomainEventHandler<MenuItemCreatedDomainEvent>

{
    readonly ICacheService _cacheService;

    public MenuItemCacheInvalidationDomainEventHandler(ICacheService cacheService)
    {
        _cacheService = cacheService;
    }

    public Task Handle(
        MenuItemPriceChangedDomainEvent notification,
        CancellationToken cancellationToken)
    {
        HandleInternal(notification.Id, cancellationToken);

        return Task.CompletedTask;
    }

    public Task Handle(
        MenuItemDeletedDomainEvent notification,
        CancellationToken cancellationToken)
    {
        HandleInternal(notification.Id, cancellationToken);

        return Task.CompletedTask;
    }

    public Task Handle(
        MenuItemCreatedDomainEvent notification,
        CancellationToken cancellationToken)
    {
        HandleInternal(notification.Id, cancellationToken);

        return Task.CompletedTask;
    }

    void HandleInternal(Guid menuItemId, CancellationToken cancellationToken)
    {
        _cacheService.Remove($"menu-items-by-id-{menuItemId}", cancellationToken);
    }
}
