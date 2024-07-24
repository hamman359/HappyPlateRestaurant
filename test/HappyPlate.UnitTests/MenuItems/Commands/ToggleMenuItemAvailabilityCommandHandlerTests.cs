using HappyPlate.Application.MenuItems.ToggleMenuItemAvailabilityCommand;
using HappyPlate.Domain.ValueObjects;


namespace HappyPlate.UnitTests.MenuItems.Commands;

public class ToggleMenuItemAvailabilityCommandHandlerTests
{
    readonly Mock<IMenuItemRepository> _menuItemRepositoryMock;
    readonly Mock<IUnitOfWork> _unitOfWorkMock;

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
            _unitOfWorkMock.Object);

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
            .ReturnsAsync(_menuItem);

        var handler = new ToggleMenuItemAvailabilityCommandHandler(
            _menuItemRepositoryMock.Object,
            _unitOfWorkMock.Object);

        var result = await handler.Handle(command, default);

        result.IsSuccess.Should().BeTrue();
        result.Value.Should().BeTrue();
    }

    [Fact]
    public async Task Handle_Should_CallGetByIdAsyncOnRepository()
    {
        var command = new ToggleMenuItemAvailabilityCommand(_menuItem.Id);

        var handler = new ToggleMenuItemAvailabilityCommandHandler(
            _menuItemRepositoryMock.Object,
            _unitOfWorkMock.Object);

        var result = await handler.Handle(command, default);

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
            _unitOfWorkMock.Object);

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
            _unitOfWorkMock.Object);

        _ = await handler.Handle(command, default);

        _unitOfWorkMock.Verify(
            x => x.SaveChangesAsync(It.IsAny<CancellationToken>()),
            Times.Once);
    }
}
