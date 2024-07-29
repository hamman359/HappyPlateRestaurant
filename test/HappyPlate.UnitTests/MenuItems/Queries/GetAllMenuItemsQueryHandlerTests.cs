using HappyPlate.Application.MenuItems.Queries.GetAllMenuItems;

namespace HappyPlate.UnitTests.MenuItems.Queries;

public class GetAllMenuItemsQueryHandlerTests
{
    readonly Mock<IMenuItemRepository> _menuItemRepositoryMock;

    public GetAllMenuItemsQueryHandlerTests()
    {
        _menuItemRepositoryMock = new();
    }

    [Fact]
    public async Task Handle_Should_CallGetAllAsyncOnRepository()
    {
        var query = new GetAllMenuItemsQuery();

        _menuItemRepositoryMock.Setup(
            x => x.GetAllAsync(It.IsAny<CancellationToken>()))
            .ReturnsAsync(new List<MenuItem>());

        var handler = new GetAllMenuItemsQueryHandler(_menuItemRepositoryMock.Object);

        _ = await handler.Handle(query, default);

        _menuItemRepositoryMock.Verify(
            x => x.GetAllAsync(It.IsAny<CancellationToken>()),
            Times.Once());
    }

    [Fact]
    public async Task Handle_Should_ReturnSuccessResult()
    {
        var query = new GetAllMenuItemsQuery();

        _menuItemRepositoryMock.Setup(
            x => x.GetAllAsync(It.IsAny<CancellationToken>()))
            .ReturnsAsync(new List<MenuItem>());

        var handler = new GetAllMenuItemsQueryHandler(_menuItemRepositoryMock.Object);

        var result = await handler.Handle(query, default);

        result.IsSuccess.Should().BeTrue();
        result.Value.Should().NotBeNull();
    }
}
