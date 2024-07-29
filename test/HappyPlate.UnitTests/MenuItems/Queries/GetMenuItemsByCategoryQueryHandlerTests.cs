using HappyPlate.Application.MenuItems.Queries.GetMenuItemsByCategory;

namespace HappyPlate.UnitTests.MenuItems.Queries;

public class GetMenuItemsByCategoryQueryHandlerTests
{
    readonly Mock<IMenuItemRepository> _menuItemRepositoryMock;

    public GetMenuItemsByCategoryQueryHandlerTests()
    {
        _menuItemRepositoryMock = new();
    }

    [Fact]
    public async Task Handle_Should_CallGetByCategoryAsyncOnRepository()
    {
        var query = new GetMenuItemsByCategoryQuery("category");

        _menuItemRepositoryMock.Setup(
            x => x.GetByCategoryAsync(It.IsAny<string>(), It.IsAny<CancellationToken>()))
            .ReturnsAsync(new List<MenuItem>());

        var handler = new GetMenuItemsByCategoryQueryHandler(_menuItemRepositoryMock.Object);

        _ = await handler.Handle(query, default);

        _menuItemRepositoryMock.Verify(
            x => x.GetByCategoryAsync(It.IsAny<string>(), It.IsAny<CancellationToken>()),
            Times.Once());
    }

    [Fact]
    public async Task Handle_Should_ReturnSuccessResult()
    {
        var query = new GetMenuItemsByCategoryQuery("category");

        _menuItemRepositoryMock.Setup(
            x => x.GetByCategoryAsync(It.IsAny<string>(), It.IsAny<CancellationToken>()))
            .ReturnsAsync(new List<MenuItem>());

        var handler = new GetMenuItemsByCategoryQueryHandler(_menuItemRepositoryMock.Object);

        var result = await handler.Handle(query, default);

        result.IsSuccess.Should().BeTrue();
        result.Value.Should().NotBeNull();
    }
}
