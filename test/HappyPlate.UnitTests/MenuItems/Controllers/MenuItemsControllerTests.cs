using HappyPlate.Application.MenuItems;
using HappyPlate.Application.MenuItems.GetAllMenuItems;
using HappyPlate.Domain.Shared;
using HappyPlate.Presentation.Controllers;

using MediatR;

using Microsoft.AspNetCore.Mvc;

namespace HappyPlate.UnitTests.MenuItems.Controllers;

public class MenuItemsControllerTests
{
    readonly Mock<ISender> _senderMock;
    readonly MenuItemsController _controller;

    public MenuItemsControllerTests()
    {
        _senderMock = new();
        _controller = new MenuItemsController(_senderMock.Object);
    }

    [Fact]
    async Task Get_Should_SendGetAllMenuItemQuery()
    {
        _senderMock.Setup(
            x => x.Send(
                It.IsAny<GetAllMenuItemsQuery>(),
                It.IsAny<CancellationToken>()))
            .ReturnsAsync(Result.Create<IList<MenuItemResponse>>(null));

        _ = await _controller.Get(default);

        _senderMock.Verify(
            x => x.Send(It.IsAny<GetAllMenuItemsQuery>(), It.IsAny<CancellationToken>()),
            Times.Once());
    }

    [Fact]
    async Task Get_Should_ReturnOkWhenResponseIsSuccessful()
    {
        IList<MenuItemResponse> result = new List<MenuItemResponse>();

        _senderMock.Setup(
            x => x.Send(
                It.IsAny<GetAllMenuItemsQuery>(),
                It.IsAny<CancellationToken>()))
            .ReturnsAsync(
                Result.Success(
                    result));

        var response = await _controller.Get(default);

        response.Should().BeOfType<OkObjectResult>();
    }
}
