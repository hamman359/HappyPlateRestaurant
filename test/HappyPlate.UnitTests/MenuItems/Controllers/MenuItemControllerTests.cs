using HappyPlate.Application.MenuItems;
using HappyPlate.Application.MenuItems.Commands.AddMenuItem;
using HappyPlate.Application.MenuItems.Commands.ChangeMenuItemPrice;
using HappyPlate.Application.MenuItems.Commands.DeleteMenuItem;
using HappyPlate.Application.MenuItems.Commands.SetMenuItemAvailable;
using HappyPlate.Application.MenuItems.Commands.SetMenuItemUnavailable;
using HappyPlate.Application.MenuItems.Queries.GetMenuItemById;
using HappyPlate.Domain.Shared;
using HappyPlate.Presentation.Controllers;

using MediatR;

using Microsoft.AspNetCore.Mvc;

namespace HappyPlate.UnitTests.MenuItems.Controllers;

public class MenuItemControllerTests
{
    readonly Mock<ISender> _senderMock;
    readonly MenuItemController _controller;

    public MenuItemControllerTests()
    {
        _senderMock = new();
        _controller = new MenuItemController(_senderMock.Object);
    }

    [Fact]
    async Task GetById_Should_SendGetMenuItemByIdQuery()
    {
        _senderMock.Setup(
            x => x.Send(
                It.IsAny<GetMenuItemByIdQuery>(),
                It.IsAny<CancellationToken>()))
            .ReturnsAsync(Result.Create<MenuItemResponse>(null));

        _ = await _controller.GetById(Guid.NewGuid(), default);

        _senderMock.Verify(
            x => x.Send(It.IsAny<GetMenuItemByIdQuery>(), It.IsAny<CancellationToken>()),
            Times.Once());
    }

    [Fact]
    async Task GetById_Should_ReturnOkWhenResponseIsSuccessful()
    {
        _senderMock.Setup(
            x => x.Send(
                It.IsAny<GetMenuItemByIdQuery>(),
                It.IsAny<CancellationToken>()))
            .ReturnsAsync(
                Result.Success(
                    new MenuItemResponse(
                        Guid.NewGuid(),
                        string.Empty,
                        string.Empty,
                        string.Empty,
                        1.0f,
                        string.Empty,
                        true)));

        var response = await _controller.GetById(Guid.NewGuid(), default);

        response.Should().BeOfType<OkObjectResult>();
    }

    [Fact]
    async Task GetById_Should_ReturnNotFoundWhenResponseIsSuccessful()
    {
        _senderMock.Setup(
            x => x.Send(
                It.IsAny<GetMenuItemByIdQuery>(),
                It.IsAny<CancellationToken>()))
            .ReturnsAsync(
                Result.Create<MenuItemResponse>(null));

        var response = await _controller.GetById(Guid.NewGuid(), default);

        response.Should().BeOfType<NotFoundObjectResult>();
    }

    [Fact]
    async Task Add_Should_SendAddMenuItemCommand()
    {
        _senderMock.Setup(
            x => x.Send(
                It.IsAny<AddMenuItemCommand>(),
                It.IsAny<CancellationToken>()))
            .ReturnsAsync(Result.Success(Guid.NewGuid()));

        _ = await _controller.Add(
            new AddMenuItemCommand(
                string.Empty,
                string.Empty,
                1.0f,
                string.Empty,
                string.Empty,
                true),
            default);

        _senderMock.Verify(
            x => x.Send(It.IsAny<AddMenuItemCommand>(), It.IsAny<CancellationToken>()),
            Times.Once());
    }

    [Fact]
    async Task Add_Should_SendReturnCreatedAtActionWhenSuccessful()
    {
        _senderMock.Setup(
            x => x.Send(
                It.IsAny<AddMenuItemCommand>(),
                It.IsAny<CancellationToken>()))
            .ReturnsAsync(Result.Success(Guid.NewGuid()));

        var response = await _controller.Add(
            new AddMenuItemCommand(
                string.Empty,
                string.Empty,
                1.0f,
                string.Empty,
                string.Empty,
                true),
            default);

        response.Should().BeOfType<CreatedAtActionResult>();
    }

    [Fact]
    async Task Add_Should_SendReturnBadRequestWhenFailure()
    {
        _senderMock.Setup(
            x => x.Send(
                It.IsAny<AddMenuItemCommand>(),
                It.IsAny<CancellationToken>()))
            .ReturnsAsync(Result.Failure<Guid>(DomainErrors.MenuItem.NotFound(Guid.NewGuid())));

        var response = await _controller.Add(
            new AddMenuItemCommand(
                string.Empty,
                string.Empty,
                1.0f,
                string.Empty,
                string.Empty,
                true),
            default);

        response.Should().BeOfType<BadRequestObjectResult>();
    }

    [Fact]
    async Task SetAsUnavailable_Should_SendToggleMenuItemAvailabilityCommand()
    {
        _senderMock.Setup(
            x => x.Send(
                It.IsAny<SetMenuItemUnavailableCommand>(),
                It.IsAny<CancellationToken>()))
            .ReturnsAsync(Result.Success(true));

        _ = await _controller.SetAsUnavailable(Guid.NewGuid(), default);

        _senderMock.Verify(
            x => x.Send(It.IsAny<SetMenuItemUnavailableCommand>(), It.IsAny<CancellationToken>()),
            Times.Once());
    }


    [Fact]
    async Task SetAsUnavailable_Should_ReturnOkWhenResponseIsSuccessful()
    {
        _senderMock.Setup(
            x => x.Send(
                It.IsAny<SetMenuItemUnavailableCommand>(),
                It.IsAny<CancellationToken>()))
            .ReturnsAsync(
                Result.Success(true));

        var response = await _controller.SetAsUnavailable(Guid.NewGuid(), default);

        response.Should().BeOfType<OkResult>();
    }

    [Fact]
    async Task SetAsUnavailable_Should_ReturnNotFoundWhenResponseIsSuccessful()
    {
        _senderMock.Setup(
            x => x.Send(
                It.IsAny<SetMenuItemUnavailableCommand>(),
                It.IsAny<CancellationToken>()))
            .ReturnsAsync(
                Result.Failure<bool>(DomainErrors.MenuItem.NotFound(Guid.NewGuid())));

        var response = await _controller.SetAsUnavailable(Guid.NewGuid(), default);

        response.Should().BeOfType<NotFoundObjectResult>();
    }


    [Fact]
    async Task SetAsAvailable_Should_SendToggleMenuItemAvailabilityCommand()
    {
        _senderMock.Setup(
            x => x.Send(
                It.IsAny<SetMenuItemAvailableCommand>(),
                It.IsAny<CancellationToken>()))
            .ReturnsAsync(Result.Success(true));

        _ = await _controller.SetAsAvailable(Guid.NewGuid(), default);

        _senderMock.Verify(
            x => x.Send(It.IsAny<SetMenuItemAvailableCommand>(), It.IsAny<CancellationToken>()),
            Times.Once());
    }


    [Fact]
    async Task SetAsAvailable_Should_ReturnOkWhenResponseIsSuccessful()
    {
        _senderMock.Setup(
            x => x.Send(
                It.IsAny<SetMenuItemAvailableCommand>(),
                It.IsAny<CancellationToken>()))
            .ReturnsAsync(
                Result.Success(true));

        var response = await _controller.SetAsAvailable(Guid.NewGuid(), default);

        response.Should().BeOfType<OkResult>();
    }

    [Fact]
    async Task SetAsAvailable_Should_ReturnNotFoundWhenResponseIsSuccessful()
    {
        _senderMock.Setup(
            x => x.Send(
                It.IsAny<SetMenuItemAvailableCommand>(),
                It.IsAny<CancellationToken>()))
            .ReturnsAsync(
                Result.Failure<bool>(DomainErrors.MenuItem.NotFound(Guid.NewGuid())));

        var response = await _controller.SetAsAvailable(Guid.NewGuid(), default);

        response.Should().BeOfType<NotFoundObjectResult>();
    }

    [Fact]
    async Task Delete_Should_SendToggleMenuItemAvailabilityCommand()
    {
        _senderMock.Setup(
            x => x.Send(
                It.IsAny<DeleteMenuItemCommand>(),
                It.IsAny<CancellationToken>()))
            .ReturnsAsync(Result.Success(true));

        _ = await _controller.Delete(Guid.NewGuid(), default);

        _senderMock.Verify(
            x => x.Send(It.IsAny<DeleteMenuItemCommand>(), It.IsAny<CancellationToken>()),
            Times.Once());
    }


    [Fact]
    async Task Delete_Should_ReturnOkWhenResponseIsSuccessful()
    {
        _senderMock.Setup(
            x => x.Send(
                It.IsAny<DeleteMenuItemCommand>(),
                It.IsAny<CancellationToken>()))
            .ReturnsAsync(
                Result.Success(true));

        var response = await _controller.Delete(Guid.NewGuid(), default);

        response.Should().BeOfType<OkResult>();
    }

    [Fact]
    async Task Delete_Should_ReturnNotFoundWhenResponseIsSuccessful()
    {
        _senderMock.Setup(
            x => x.Send(
                It.IsAny<DeleteMenuItemCommand>(),
                It.IsAny<CancellationToken>()))
            .ReturnsAsync(
                Result.Failure<bool>(DomainErrors.MenuItem.NotFound(Guid.NewGuid())));

        var response = await _controller.Delete(Guid.NewGuid(), default);

        response.Should().BeOfType<NotFoundObjectResult>();
    }


    [Fact]
    async Task ChangePrice_Should_SendChangeMenuItemPriceCommand()
    {
        _senderMock.Setup(
            x => x.Send(
                It.IsAny<ChangeMenuItemPriceCommand>(),
                It.IsAny<CancellationToken>()))
            .ReturnsAsync(Result.Success(Guid.NewGuid()));

        _ = await _controller.ChangePrice(
            new ChangeMenuItemPriceCommand(Guid.NewGuid(), 1.0f),
            default);

        _senderMock.Verify(
            x => x.Send(It.IsAny<ChangeMenuItemPriceCommand>(), It.IsAny<CancellationToken>()),
            Times.Once());
    }


    [Fact]
    async Task ChangePrice_Should_ReturnOkWhenResponseIsSuccessful()
    {
        _senderMock.Setup(
            x => x.Send(
                It.IsAny<ChangeMenuItemPriceCommand>(),
                It.IsAny<CancellationToken>()))
            .ReturnsAsync(
                Result.Success(Guid.NewGuid()));

        var response = await _controller.ChangePrice(
            new ChangeMenuItemPriceCommand(Guid.NewGuid(), 1.0f),
            default);

        response.Should().BeOfType<OkResult>();
    }

    [Fact]
    async Task ChangePrice_Should_ReturnNotFoundWhenMenuItemDoesNotExist()
    {
        _senderMock.Setup(
            x => x.Send(
                It.IsAny<ChangeMenuItemPriceCommand>(),
                It.IsAny<CancellationToken>()))
            .ReturnsAsync(
                Result.Failure<Guid>(DomainErrors.MenuItem.NotFound(Guid.NewGuid())));

        var response = await _controller.ChangePrice(
            new ChangeMenuItemPriceCommand(Guid.NewGuid(), 1.0f),
            default);

        //TODO: Update and fix
        response.Should().BeOfType<BadRequestObjectResult>();
    }

    [Fact]
    async Task ChangePrice_Should_ReturnBadRequestWhenPriceIsInvalid()
    {
        _senderMock.Setup(
            x => x.Send(
                It.IsAny<ChangeMenuItemPriceCommand>(),
                It.IsAny<CancellationToken>()))
            .ReturnsAsync(
                Result.Failure<Guid>(DomainErrors.Price.Negative));

        var response = await _controller.ChangePrice(
            new ChangeMenuItemPriceCommand(Guid.NewGuid(), -1.0f),
            default);

        response.Should().BeOfType<BadRequestObjectResult>();
    }
}
