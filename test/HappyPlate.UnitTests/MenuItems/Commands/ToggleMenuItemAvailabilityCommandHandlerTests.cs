﻿using HappyPlate.Application.MenuItems.ToggleMenuItemAvailability;
using HappyPlate.Domain.DomainEvents;
using HappyPlate.Domain.ValueObjects;

using MediatR;


namespace HappyPlate.UnitTests.MenuItems.Commands;

public class ToggleMenuItemAvailabilityCommandHandlerTests
{
    readonly Mock<IMenuItemRepository> _menuItemRepositoryMock;
    readonly Mock<IUnitOfWork> _unitOfWorkMock;
    readonly Mock<IPublisher> _publisherMock;

    readonly MenuItem _menuItem = MenuItem.Create(
        MenuItemName.Create("Name").Value,
        "Description",
        Price.Create(1.0f).Value,
        "Category",
        "Image",
        true);

    public ToggleMenuItemAvailabilityCommandHandlerTests()
    {
        _menuItemRepositoryMock = new();
        _unitOfWorkMock = new();
        _publisherMock = new();
    }

    [Fact]
    public async Task Handle_Should_ReturnSuccessResult_WhenMenuItemIsFound()
    {
        var command = new ToggleMenuItemAvailabilityCommand(_menuItem.Id);

        _menuItemRepositoryMock.Setup(
            x => x.GetByIdAsync(
                command.MenuItemId,
                It.IsAny<CancellationToken>()))
            .ReturnsAsync(_menuItem);

        var handler = new ToggleMenuItemAvailabilityCommandHandler(
            _menuItemRepositoryMock.Object,
            _unitOfWorkMock.Object,
            _publisherMock.Object);

        var result = await handler.Handle(command, default);

        result.IsSuccess.Should().BeTrue();
        result.Value.Should().BeTrue();
    }

    [Fact]
    public async Task Handle_Should_ReturnFailureResult_WhenMenuItemIsNotFound()
    {
        var command = new ToggleMenuItemAvailabilityCommand(_menuItem.Id);

        _menuItemRepositoryMock.Setup(
            x => x.GetByIdAsync(
                command.MenuItemId,
                It.IsAny<CancellationToken>()))
            .ReturnsAsync((MenuItem?)null);

        var handler = new ToggleMenuItemAvailabilityCommandHandler(
            _menuItemRepositoryMock.Object,
            _unitOfWorkMock.Object,
            _publisherMock.Object);

        var result = await handler.Handle(command, default);

        result.IsFailure.Should().BeTrue();
    }

    [Fact]
    public async Task Handle_Should_CallGetByIdAsyncOnRepository()
    {
        var command = new ToggleMenuItemAvailabilityCommand(_menuItem.Id);

        var handler = new ToggleMenuItemAvailabilityCommandHandler(
            _menuItemRepositoryMock.Object,
            _unitOfWorkMock.Object,
            _publisherMock.Object);

        _ = await handler.Handle(command, default);

        _menuItemRepositoryMock.Verify(
            x => x.GetByIdAsync(_menuItem.Id, It.IsAny<CancellationToken>()),
            Times.Once());
    }

    [Fact]
    public async Task Handle_Should_NotCallUnitOfWork_WhenMenuItemIsNotFound()
    {
        var command = new ToggleMenuItemAvailabilityCommand(_menuItem.Id);

        _menuItemRepositoryMock.Setup(
            x => x.GetByIdAsync(
                command.MenuItemId,
                It.IsAny<CancellationToken>()))
            .ReturnsAsync((MenuItem?)null);

        var handler = new ToggleMenuItemAvailabilityCommandHandler(
            _menuItemRepositoryMock.Object,
            _unitOfWorkMock.Object,
            _publisherMock.Object);

        _ = await handler.Handle(command, default);

        _unitOfWorkMock.Verify(
            x => x.SaveChangesAsync(It.IsAny<CancellationToken>()),
            Times.Never);
    }

    [Fact]
    public async Task Handle_Should_CallUnitOfWork_WhenMenuItemIsFound()
    {
        var command = new ToggleMenuItemAvailabilityCommand(_menuItem.Id);

        _menuItemRepositoryMock.Setup(
            x => x.GetByIdAsync(
                command.MenuItemId,
                It.IsAny<CancellationToken>()))
            .ReturnsAsync(_menuItem);

        var handler = new ToggleMenuItemAvailabilityCommandHandler(
            _menuItemRepositoryMock.Object,
            _unitOfWorkMock.Object,
            _publisherMock.Object);

        _ = await handler.Handle(command, default);

        _unitOfWorkMock.Verify(
            x => x.SaveChangesAsync(It.IsAny<CancellationToken>()),
            Times.Once);
    }

    [Fact]
    public async Task Handle_Should_PublishMenuItemUpdatedDomainEvent_WhenMenuItemIsFound()
    {
        var command = new ToggleMenuItemAvailabilityCommand(_menuItem.Id);

        _menuItemRepositoryMock.Setup(
            x => x.GetByIdAsync(
                command.MenuItemId,
                It.IsAny<CancellationToken>()))
            .ReturnsAsync(_menuItem);

        var handler = new ToggleMenuItemAvailabilityCommandHandler(
            _menuItemRepositoryMock.Object,
            _unitOfWorkMock.Object,
            _publisherMock.Object);

        _ = await handler.Handle(command, default);

        _publisherMock.Verify(
            x => x.Publish(
                It.IsAny<MenuItemUpdatedDomainEvent>(),
                It.IsAny<CancellationToken>()),
            Times.Once);
    }

    [Fact]
    public async Task Handle_Should_NotPublishMenuItemUpdatedDomainEvent_WhenMenuItemIsNotFound()
    {
        var command = new ToggleMenuItemAvailabilityCommand(_menuItem.Id);

        _menuItemRepositoryMock.Setup(
            x => x.GetByIdAsync(
                command.MenuItemId,
                It.IsAny<CancellationToken>()))
            .ReturnsAsync((MenuItem?)null);

        var handler = new ToggleMenuItemAvailabilityCommandHandler(
            _menuItemRepositoryMock.Object,
            _unitOfWorkMock.Object,
            _publisherMock.Object);

        _ = await handler.Handle(command, default);

        _publisherMock.Verify(
            x => x.Publish(
                It.IsAny<MenuItemUpdatedDomainEvent>(),
                It.IsAny<CancellationToken>()),
            Times.Never);
    }
}
