using HappyPlate.Application.Customers.Commands.DeleteCustomer;
using HappyPlate.Domain.DomainEvents;
using HappyPlate.Domain.ValueObjects;

using MediatR;


namespace HappyPlate.UnitTests.Customers.Commands;

public class DeleteCustomerCommandHandlerTests
{
    readonly Mock<ICustomerRepository> _CustomerRepositoryMock;
    readonly Mock<IUnitOfWork> _unitOfWorkMock;
    readonly Mock<IPublisher> _publisherMock;

    readonly Customer _Customer = Customer.Create(
        FirstName.Create("Name").Value,
        LastName.Create("Name").Value,
        Email.Create("Test@email.com").Value,
        PhoneNumber.Create("111", "222", "3333", null).Value,
        new List<Address>());

    public DeleteCustomerCommandHandlerTests()
    {
        _CustomerRepositoryMock = new();
        _unitOfWorkMock = new();
        _publisherMock = new();
    }

    [Fact]
    public async Task Handle_Should_ReturnSuccessResult_WhenCustomerIsFound()
    {
        var command = new DeleteCustomerCommand(_Customer.Id);

        _CustomerRepositoryMock.Setup(
            x => x.GetByIdAsync(
                command.CustomerId,
                It.IsAny<CancellationToken>()))
            .ReturnsAsync(_Customer);

        var handler = new DeleteCustomerCommandHandler(
            _CustomerRepositoryMock.Object,
            _unitOfWorkMock.Object,
            _publisherMock.Object);

        var result = await handler.Handle(command, default);

        result.IsSuccess.Should().BeTrue();
        result.Value.Should().BeTrue();
    }

    [Fact]
    public async Task Handle_Should_ReturnFailureResult_WhenCustomerIsNotFound()
    {
        var command = new DeleteCustomerCommand(_Customer.Id);

        _CustomerRepositoryMock.Setup(
            x => x.GetByIdAsync(
                command.CustomerId,
                It.IsAny<CancellationToken>()))
            .ReturnsAsync((Customer?)null);

        var handler = new DeleteCustomerCommandHandler(
            _CustomerRepositoryMock.Object,
            _unitOfWorkMock.Object,
            _publisherMock.Object);

        var result = await handler.Handle(command, default);

        result.IsFailure.Should().BeTrue();
    }

    [Fact]
    public async Task Handle_Should_CallGetByIdAsyncOnRepository()
    {
        var command = new DeleteCustomerCommand(_Customer.Id);

        var handler = new DeleteCustomerCommandHandler(
            _CustomerRepositoryMock.Object,
            _unitOfWorkMock.Object,
            _publisherMock.Object);

        _ = await handler.Handle(command, default);

        _CustomerRepositoryMock.Verify(
            x => x.GetByIdAsync(_Customer.Id, It.IsAny<CancellationToken>()),
            Times.Once());
    }

    [Fact]
    public async Task Handle_Should_NotCallUnitOfWork_WhenCustomerIsNotFound()
    {
        var command = new DeleteCustomerCommand(_Customer.Id);

        _CustomerRepositoryMock.Setup(
            x => x.GetByIdAsync(
                command.CustomerId,
                It.IsAny<CancellationToken>()))
            .ReturnsAsync((Customer?)null);

        var handler = new DeleteCustomerCommandHandler(
            _CustomerRepositoryMock.Object,
            _unitOfWorkMock.Object,
            _publisherMock.Object);

        _ = await handler.Handle(command, default);

        _unitOfWorkMock.Verify(
            x => x.SaveChangesAsync(It.IsAny<CancellationToken>()),
            Times.Never);
    }

    [Fact]
    public async Task Handle_Should_CallUnitOfWork_WhenCustomerIsFound()
    {
        var command = new DeleteCustomerCommand(_Customer.Id);

        _CustomerRepositoryMock.Setup(
            x => x.GetByIdAsync(
                command.CustomerId,
                It.IsAny<CancellationToken>()))
            .ReturnsAsync(_Customer);

        var handler = new DeleteCustomerCommandHandler(
            _CustomerRepositoryMock.Object,
            _unitOfWorkMock.Object,
            _publisherMock.Object);

        _ = await handler.Handle(command, default);

        _unitOfWorkMock.Verify(
            x => x.SaveChangesAsync(It.IsAny<CancellationToken>()),
            Times.Once);
    }

    [Fact]
    public async Task Handle_Should_PublishCustomerDeletedDomainEvent_WhenCustomerIsFound()
    {
        var command = new DeleteCustomerCommand(_Customer.Id);

        _CustomerRepositoryMock.Setup(
            x => x.GetByIdAsync(
                command.CustomerId,
                It.IsAny<CancellationToken>()))
            .ReturnsAsync(_Customer);

        var handler = new DeleteCustomerCommandHandler(
            _CustomerRepositoryMock.Object,
            _unitOfWorkMock.Object,
            _publisherMock.Object);

        _ = await handler.Handle(command, default);

        _publisherMock.Verify(
            x => x.Publish(
                It.IsAny<CustomerDeletedDomainEvent>(),
                It.IsAny<CancellationToken>()),
            Times.Once);
    }

    [Fact]
    public async Task Handle_Should_NotPublishCustomerDeletedDomainEvent_WhenCustomerIsNotFound()
    {
        var command = new DeleteCustomerCommand(_Customer.Id);

        _CustomerRepositoryMock.Setup(
            x => x.GetByIdAsync(
                command.CustomerId,
                It.IsAny<CancellationToken>()))
            .ReturnsAsync((Customer?)null);

        var handler = new DeleteCustomerCommandHandler(
            _CustomerRepositoryMock.Object,
            _unitOfWorkMock.Object,
            _publisherMock.Object);

        _ = await handler.Handle(command, default);

        _publisherMock.Verify(
            x => x.Publish(
                It.IsAny<CustomerDeletedDomainEvent>(),
                It.IsAny<CancellationToken>()),
            Times.Never);
    }
}
