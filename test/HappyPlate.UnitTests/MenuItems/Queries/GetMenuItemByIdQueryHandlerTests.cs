using HappyPlate.Application.MenuItems.GetMenuItemById;
using HappyPlate.Domain.ValueObjects;

namespace HappyPlate.UnitTests.MenuItems.Queries;

public class GetMenuItemByIdQueryHandlerTests
{
    readonly Mock<IMenuItemRepository> _menuItemRepositoryMock;
    readonly MenuItem _menuItem = MenuItem.Create(
        MenuItemName.Create("Name").Value,
        "Description",
        Price.Create(1.0f).Value,
        "Category",
        "Image",
        true);

    public GetMenuItemByIdQueryHandlerTests()
    {
        _menuItemRepositoryMock = new();
    }

    [Fact]
    public async Task Handle_Should_CallGetByIdAsyncOnRepository()
    {
        var query = new GetMenuItemByIdQuery(_menuItem.Id);

        var handler = new GetMenuItemByIdQueryHandler(_menuItemRepositoryMock.Object);

        _ = await handler.Handle(query, default);

        _menuItemRepositoryMock.Verify(
            x => x.GetByIdAsync(_menuItem.Id, It.IsAny<CancellationToken>()),
            Times.Once());
    }

    [Fact]
    public async Task Handle_Should_ReturnFailureResult_WhenMenuItemIsNotFound()
    {
        var query = new GetMenuItemByIdQuery(Guid.NewGuid());

        _menuItemRepositoryMock.Setup(
            x => x.GetByIdAsync(
                query.MenuItemId,
                It.IsAny<CancellationToken>()))
            .ReturnsAsync((MenuItem?)null);

        var handler = new GetMenuItemByIdQueryHandler(_menuItemRepositoryMock.Object);

        var result = await handler.Handle(query, default);

        result.IsFailure.Should().BeTrue();
        result.Error.Should().Be(DomainErrors.MenuItem.NotFound(query.MenuItemId));
    }

    [Fact]
    public async Task Handle_Should_ReturnSuccessResult_WhenMenuItemIsFound()
    {
        var query = new GetMenuItemByIdQuery(_menuItem.Id);

        _menuItemRepositoryMock.Setup(
            x => x.GetByIdAsync(
                query.MenuItemId,
                It.IsAny<CancellationToken>()))
            .ReturnsAsync(_menuItem);

        var handler = new GetMenuItemByIdQueryHandler(_menuItemRepositoryMock.Object);

        var result = await handler.Handle(query, default);

        result.IsSuccess.Should().BeTrue();
        result.Value.Should().NotBeNull();
    }
}
