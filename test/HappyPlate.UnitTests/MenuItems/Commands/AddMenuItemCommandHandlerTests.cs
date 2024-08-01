using HappyPlate.Application.MenuItems.Commands.AddMenuItem;
using HappyPlate.Domain.DomainEvents;

using MediatR;


namespace HappyPlate.UnitTests.MenuItems.Commands;

public class AddMenuItemCommandHandlerTests
{
    readonly Mock<IMenuItemRepository> _menuItemRepositoryMock;
    readonly Mock<IUnitOfWork> _unitOfWorkMock;
    readonly Mock<IPublisher> _publisherMock;

    public AddMenuItemCommandHandlerTests()
    {
        _menuItemRepositoryMock = new();
        _unitOfWorkMock = new();
        _publisherMock = new();
    }

    [Fact]
    public async Task Handle_Should_ReturnFailureResult_WhenPriceIsNegative()
    {
        var command = new AddMenuItemCommand("Product", "Description", -1.0f, "Category", "Image", true);

        var handler = new AddMenuItemCommandHandler(
            _menuItemRepositoryMock.Object,
            _unitOfWorkMock.Object,
            _publisherMock.Object);

        var result = await handler.Handle(command, default);

        result.IsFailure.Should().BeTrue();
        result.Error.Should().Be(DomainErrors.Price.Negative);
    }

    [Fact]
    public async Task Handle_Should_ReturnFailureResult_WhenNameIsEmpty()
    {
        var command = new AddMenuItemCommand("", "Description", 1.0f, "Category", "Image", true);

        var handler = new AddMenuItemCommandHandler(
            _menuItemRepositoryMock.Object,
            _unitOfWorkMock.Object,
            _publisherMock.Object);

        var result = await handler.Handle(command, default);

        result.IsFailure.Should().BeTrue();
        result.Error.Should().Be(DomainErrors.MenuItemName.Empty);
    }

    [Fact]
    public async Task Handle_Should_CallAddOnRepository_WhenAllDataIsValid()
    {
        var command = new AddMenuItemCommand("Product", "Description", 1.0f, "Category", "Image", true);

        var handler = new AddMenuItemCommandHandler(
            _menuItemRepositoryMock.Object,
            _unitOfWorkMock.Object,
            _publisherMock.Object);

        var result = await handler.Handle(command, default);

        _menuItemRepositoryMock.Verify(
            x => x.Add(
                It.Is<MenuItem>(p => p.Id == result.Value)),
                Times.Once);
    }

    [Fact]
    public async Task Handle_Should_NotCallUnitOfWork_WhenPriceIsNegative()
    {
        var command = new AddMenuItemCommand("Product", "Description", -1.0f, "Category", "Image", true);

        var handler = new AddMenuItemCommandHandler(
            _menuItemRepositoryMock.Object,
            _unitOfWorkMock.Object,
            _publisherMock.Object);

        _ = await handler.Handle(command, default);

        _unitOfWorkMock.Verify(
            x => x.SaveChangesAsync(It.IsAny<CancellationToken>()),
            Times.Never);
    }

    [Fact]
    public async Task Handle_Should_CallUnitOfWorkSaveChangesAsync_WhenAllDataIsValid()
    {
        var command = new AddMenuItemCommand("Product", "Description", 1.0f, "Category", "Image", true);

        var handler = new AddMenuItemCommandHandler(
            _menuItemRepositoryMock.Object,
            _unitOfWorkMock.Object,
            _publisherMock.Object);

        _ = await handler.Handle(command, default);

        _unitOfWorkMock.Verify(
            x => x.SaveChangesAsync(It.IsAny<CancellationToken>()),
            Times.Once);
    }

    [Fact]
    public async Task Handle_Should_PublishMenuItemCreatedDomainEvent_WhenAllDataIsValid()
    {
        var command = new AddMenuItemCommand("Product", "Description", 1.0f, "Category", "Image", true);

        var handler = new AddMenuItemCommandHandler(
            _menuItemRepositoryMock.Object,
            _unitOfWorkMock.Object,
            _publisherMock.Object);

        _ = await handler.Handle(command, default);

        _publisherMock.Verify(
            x => x.Publish(
                It.IsAny<MenuItemCreatedDomainEvent>(),
                It.IsAny<CancellationToken>()),
            Times.Once);
    }

    [Fact]
    public async Task Handle_Should_NotPublishMenuItemCreatedDomainEvent_WhenPriceIsNegative()
    {
        var command = new AddMenuItemCommand("Product", "Description", -1.0f, "Category", "Image", true);

        var handler = new AddMenuItemCommandHandler(
            _menuItemRepositoryMock.Object,
            _unitOfWorkMock.Object,
            _publisherMock.Object);

        _ = await handler.Handle(command, default);

        _publisherMock.Verify(
            x => x.Publish(
                It.IsAny<MenuItemCreatedDomainEvent>(),
                It.IsAny<CancellationToken>()),
            Times.Never);
    }
}
