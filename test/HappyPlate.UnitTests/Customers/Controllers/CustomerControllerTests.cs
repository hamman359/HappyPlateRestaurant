using HappyPlate.Application.Customers;
using HappyPlate.Application.Customers.Commands.AddCustomer;
using HappyPlate.Application.Customers.Commands.DeleteCustomer;
using HappyPlate.Application.Customers.Queries;
using HappyPlate.Domain.Shared;
using HappyPlate.Presentation.Controllers;

using MediatR;

using Microsoft.AspNetCore.Mvc;

namespace HappyPlate.UnitTests.Customers.Controllers;

public class CustomerControllerTests
{
    readonly Mock<ISender> _senderMock;
    readonly CustomerController _controller;

    public CustomerControllerTests()
    {
        _senderMock = new();
        _controller = new CustomerController(_senderMock.Object);
    }

    [Fact]
    async Task GetById_Should_SendGetCustomerByIdQuery()
    {
        _senderMock.Setup(
            x => x.Send(
                It.IsAny<GetCustomerByIdQuery>(),
                It.IsAny<CancellationToken>()))
            .ReturnsAsync(Result.Create<CustomerResponse>(null));

        _ = await _controller.GetById(Guid.NewGuid(), default);

        _senderMock.Verify(
            x => x.Send(It.IsAny<GetCustomerByIdQuery>(), It.IsAny<CancellationToken>()),
            Times.Once());
    }

    [Fact]
    async Task GetById_Should_ReturnOkWhenResponseIsSuccessful()
    {
        _senderMock.Setup(
            x => x.Send(
                It.IsAny<GetCustomerByIdQuery>(),
                It.IsAny<CancellationToken>()))
            .ReturnsAsync(
                Result.Success(
                    new CustomerResponse(
                        "First",
                        "Last",
                        "Email",
                        "(111) 222 3333",
                        null)));

        var response = await _controller.GetById(Guid.NewGuid(), default);

        response.Should().BeOfType<OkObjectResult>();
    }

    [Fact]
    async Task GetById_Should_ReturnNotFoundWhenResponseIsSuccessful()
    {
        _senderMock.Setup(
            x => x.Send(
                It.IsAny<GetCustomerByIdQuery>(),
                It.IsAny<CancellationToken>()))
            .ReturnsAsync(
                Result.Create<CustomerResponse>(null));

        var response = await _controller.GetById(Guid.NewGuid(), default);

        response.Should().BeOfType<NotFoundObjectResult>();
    }

    [Fact]
    async Task Add_Should_SendAddCustomerCommand()
    {
        _senderMock.Setup(
            x => x.Send(
                It.IsAny<AddCustomerCommand>(),
                It.IsAny<CancellationToken>()))
            .ReturnsAsync(Result.Success(Guid.NewGuid()));

        _ = await _controller.Add(
            new AddCustomerCommand(
                "First",
                "Last",
                "Email",
                "111",
                "222",
                "3333",
                null,
                new List<AddressDto>()),
            default);

        _senderMock.Verify(
            x => x.Send(It.IsAny<AddCustomerCommand>(), It.IsAny<CancellationToken>()),
            Times.Once());
    }

    [Fact]
    async Task Add_Should_SendReturnCreatedAtActionWhenSuccessful()
    {
        _senderMock.Setup(
            x => x.Send(
                It.IsAny<AddCustomerCommand>(),
                It.IsAny<CancellationToken>()))
            .ReturnsAsync(Result.Success(Guid.NewGuid()));

        var response = await _controller.Add(
            new AddCustomerCommand(
                "First",
                "Last",
                "Email",
                "111",
                "222",
                "3333",
                null,
                new List<AddressDto>()),
            default);

        response.Should().BeOfType<CreatedAtActionResult>();
    }

    [Fact]
    async Task Add_Should_SendReturnBadRequestWhenFailure()
    {
        _senderMock.Setup(
            x => x.Send(
                It.IsAny<AddCustomerCommand>(),
                It.IsAny<CancellationToken>()))
            .ReturnsAsync(Result.Failure<Guid>(DomainErrors.Customer.NotFound(Guid.NewGuid())));

        var response = await _controller.Add(
            new AddCustomerCommand(
                "First",
                "Last",
                "Email",
                "111",
                "222",
                "3333",
                null,
                new List<AddressDto>()),
            default);

        response.Should().BeOfType<BadRequestObjectResult>();
    }


    [Fact]
    async Task Delete_Should_SendDeleteCustomerCommand()
    {
        _senderMock.Setup(
            x => x.Send(
                It.IsAny<DeleteCustomerCommand>(),
                It.IsAny<CancellationToken>()))
            .ReturnsAsync(Result.Success(true));

        _ = await _controller.Delete(Guid.NewGuid(), default);

        _senderMock.Verify(
            x => x.Send(It.IsAny<DeleteCustomerCommand>(), It.IsAny<CancellationToken>()),
            Times.Once());
    }


    [Fact]
    async Task Delete_Should_ReturnOkWhenResponseIsSuccessful()
    {
        _senderMock.Setup(
            x => x.Send(
                It.IsAny<DeleteCustomerCommand>(),
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
                It.IsAny<DeleteCustomerCommand>(),
                It.IsAny<CancellationToken>()))
            .ReturnsAsync(
                Result.Failure<bool>(DomainErrors.Customer.NotFound(Guid.NewGuid())));

        var response = await _controller.Delete(Guid.NewGuid(), default);

        response.Should().BeOfType<NotFoundObjectResult>();
    }
}
