using HappyPlate.Application.MenuItems.AddMenuItem;


namespace HappyPlate.UnitTests.MenuItems.Commands;

public class AddMenuItemCommandHandlerTests
{
    readonly Mock<IMenuItemRepository> _menuItemRepositoryMock;
    readonly Mock<IUnitOfWork> _unitOfWorkMock;

    public AddMenuItemCommandHandlerTests()
    {
        _menuItemRepositoryMock = new();
        _unitOfWorkMock = new();
    }

    [Fact]
    public async Task Handle_Should_ReturnFailureResult_WhenPriceIsNegative()
    {
        var command = new AddMenuItemCommand("Product", "Description", -1.0f, "Category", "Image", true);

        var handler = new AddMenuItemCommandHandler(
            _menuItemRepositoryMock.Object,
            _unitOfWorkMock.Object);

        var result = await handler.Handle(command, default);

        result.IsFailure.Should().BeTrue();
        result.Error.Should().Be(DomainErrors.Price.Negative);
    }

    [Fact]
    public async Task Handle_Should_CallAddOnRepository_WhenPriceIsValid()
    {
        var command = new AddMenuItemCommand("Product", "Description", 1.0f, "Category", "Image", true);

        var handler = new AddMenuItemCommandHandler(
            _menuItemRepositoryMock.Object,
            _unitOfWorkMock.Object);

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
            _unitOfWorkMock.Object);

        _ = await handler.Handle(command, default);

        _unitOfWorkMock.Verify(
            x => x.SaveChangesAsync(It.IsAny<CancellationToken>()),
            Times.Never);
    }

    [Fact]
    public async Task Handle_Should_CallUnitOfWork_WhenPriceIsValid()
    {
        var command = new AddMenuItemCommand("Product", "Description", 1.0f, "Category", "Image", true);

        var handler = new AddMenuItemCommandHandler(
            _menuItemRepositoryMock.Object,
            _unitOfWorkMock.Object);

        _ = await handler.Handle(command, default);

        _unitOfWorkMock.Verify(
            x => x.SaveChangesAsync(It.IsAny<CancellationToken>()),
            Times.Once);
    }
}
