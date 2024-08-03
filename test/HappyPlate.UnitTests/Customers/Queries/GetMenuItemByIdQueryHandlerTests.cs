using HappyPlate.Application.Customers.Queries;
using HappyPlate.Domain.ValueObjects;

namespace HappyPlate.UnitTests.Customers.Queries;

public class GetCustomerByIdQueryHandlerTests
{
    readonly Mock<ICustomerRepository> _CustomerRepositoryMock;

    readonly Customer _Customer = Customer.Create(
        FirstName.Create("Name").Value,
        LastName.Create("Name").Value,
        Email.Create("Test@email.com").Value,
        PhoneNumber.Create("111", "222", "3333", null).Value,
        new List<Address>());

    public GetCustomerByIdQueryHandlerTests()
    {
        _CustomerRepositoryMock = new();
    }

    [Fact]
    public async Task Handle_Should_CallGetByIdAsyncOnRepository()
    {
        var query = new GetCustomerByIdQuery(_Customer.Id);

        var handler = new GetCustomerByIdQueryHandler(_CustomerRepositoryMock.Object);

        _ = await handler.Handle(query, default);

        _CustomerRepositoryMock.Verify(
            x => x.GetByIdAsync(_Customer.Id, It.IsAny<CancellationToken>()),
            Times.Once());
    }

    [Fact]
    public async Task Handle_Should_ReturnFailureResult_WhenCustomerIsNotFound()
    {
        var query = new GetCustomerByIdQuery(Guid.NewGuid());

        _CustomerRepositoryMock.Setup(
            x => x.GetByIdAsync(
                query.CustomerId,
                It.IsAny<CancellationToken>()))
            .ReturnsAsync((Customer?)null);

        var handler = new GetCustomerByIdQueryHandler(_CustomerRepositoryMock.Object);

        var result = await handler.Handle(query, default);

        result.IsFailure.Should().BeTrue();
        result.Error.Should().Be(DomainErrors.Customer.NotFound(query.CustomerId));
    }

    [Fact]
    public async Task Handle_Should_ReturnSuccessResult_WhenCustomerIsFound()
    {
        var query = new GetCustomerByIdQuery(_Customer.Id);

        _CustomerRepositoryMock.Setup(
            x => x.GetByIdAsync(
                query.CustomerId,
                It.IsAny<CancellationToken>()))
            .ReturnsAsync(_Customer);

        var handler = new GetCustomerByIdQueryHandler(_CustomerRepositoryMock.Object);

        var result = await handler.Handle(query, default);

        result.IsSuccess.Should().BeTrue();
        result.Value.Should().NotBeNull();
    }
}
