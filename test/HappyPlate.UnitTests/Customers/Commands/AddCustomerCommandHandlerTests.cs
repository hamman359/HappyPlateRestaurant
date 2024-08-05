using HappyPlate.Application.Customers.Commands.AddCustomer;

namespace HappyPlate.UnitTests.Customers.Commands;

public class AddCustomerCommandHandlerTests
{
    readonly Mock<ICustomerRepository> _customerRepositoryMock;
    readonly Mock<IUnitOfWork> _unitOfWorkMock;

    public AddCustomerCommandHandlerTests()
    {
        _customerRepositoryMock = new();
        _unitOfWorkMock = new();
    }

    [Fact]
    public async Task Handle_Should_ReturnFailureResult_WhenFirstNameIsEmpty()
    {
        var command = new AddCustomerCommand(
            string.Empty,
            "LastName",
            "test@email.com",
            "123",
            "456",
            "7890",
            null,
            new List<AddressDto>());

        var handler = new AddCustomerCommandHandler(
            _customerRepositoryMock.Object,
            _unitOfWorkMock.Object);

        var result = await handler.Handle(command, default);

        result.IsFailure.Should().BeTrue();
        result.Error.Should().Be(DomainErrors.FirstName.Empty);
    }

    [Fact]
    public async Task Handle_Should_ReturnFailureResult_WhenFirstNameIsTooLong()
    {
        var command = new AddCustomerCommand(
            new string('a', 51),
            "LastName",
            "test@email.com",
            "123",
            "456",
            "7890",
            null,
            new List<AddressDto>());

        var handler = new AddCustomerCommandHandler(
            _customerRepositoryMock.Object,
            _unitOfWorkMock.Object);

        var result = await handler.Handle(command, default);

        result.IsFailure.Should().BeTrue();
        result.Error.Should().Be(DomainErrors.FirstName.TooLong);
    }


    [Fact]
    public async Task Handle_Should_ReturnFailureResult_WhenLastNameIsEmpty()
    {
        var command = new AddCustomerCommand(
            "FirstName",
            string.Empty,
            "test@email.com",
            "123",
            "456",
            "7890",
            null,
            new List<AddressDto>());

        var handler = new AddCustomerCommandHandler(
            _customerRepositoryMock.Object,
            _unitOfWorkMock.Object);

        var result = await handler.Handle(command, default);

        result.IsFailure.Should().BeTrue();
        result.Error.Should().Be(DomainErrors.LastName.Empty);
    }

    [Fact]
    public async Task Handle_Should_ReturnFailureResult_WhenLastNameIsTooLong()
    {
        var command = new AddCustomerCommand(
            "FirstName",
            new string('a', 51),
            "test@email.com",
            "123",
            "456",
            "7890",
            null,
            new List<AddressDto>());

        var handler = new AddCustomerCommandHandler(
            _customerRepositoryMock.Object,
            _unitOfWorkMock.Object);

        var result = await handler.Handle(command, default);

        result.IsFailure.Should().BeTrue();
        result.Error.Should().Be(DomainErrors.LastName.TooLong);
    }


    [Fact]
    public async Task Handle_Should_ReturnFailureResult_WhenEmailIsEmpty()
    {
        var command = new AddCustomerCommand(
            "FirstName",
            "LastName",
            string.Empty,
            "123",
            "456",
            "7890",
            null,
            new List<AddressDto>());

        var handler = new AddCustomerCommandHandler(
            _customerRepositoryMock.Object,
            _unitOfWorkMock.Object);

        var result = await handler.Handle(command, default);

        result.IsFailure.Should().BeTrue();
        result.Error.Should().Be(DomainErrors.Email.Empty);
    }

    [Fact]
    public async Task Handle_Should_ReturnFailureResult_WhenEmailIsTooLong()
    {
        var command = new AddCustomerCommand(
            "FirstName",
            "LastName",
            $"{new string('a', 250)}@email.com",
            "123",
            "456",
            "7890",
            null,
            new List<AddressDto>());

        var handler = new AddCustomerCommandHandler(
            _customerRepositoryMock.Object,
            _unitOfWorkMock.Object);

        var result = await handler.Handle(command, default);

        result.IsFailure.Should().BeTrue();
        result.Error.Should().Be(DomainErrors.Email.TooLong);
    }

    [Fact]
    public async Task Handle_Should_ReturnFailureResult_WhenEmailIsInvalidlyFormatted()
    {
        var command = new AddCustomerCommand(
            "FirstName",
            "LastName",
            "bademail.com",
            "123",
            "456",
            "7890",
            null,
            new List<AddressDto>());

        var handler = new AddCustomerCommandHandler(
            _customerRepositoryMock.Object,
            _unitOfWorkMock.Object);

        var result = await handler.Handle(command, default);

        result.IsFailure.Should().BeTrue();
        result.Error.Should().Be(DomainErrors.Email.InvalidFormat);
    }

    [Fact]
    public async Task Handle_Should_ReturnFailureResult_WhenAreaCodeIsEmpty()
    {
        var command = new AddCustomerCommand(
            "FirstName",
            "LastName",
            "test@email.com",
            string.Empty,
            "456",
            "7890",
            null,
            new List<AddressDto>());

        var handler = new AddCustomerCommandHandler(
            _customerRepositoryMock.Object,
            _unitOfWorkMock.Object);

        var result = await handler.Handle(command, default);

        result.IsFailure.Should().BeTrue();
        result.Error.Should().Be(DomainErrors.PhoneNumber.AreaCodeEmpty);
    }

    [Theory]
    [InlineData("ABC")]
    [InlineData("!23")]
    public async Task Handle_Should_ReturnFailureResult_WhenAreaCodeIsNotNumber(string areaCode)
    {
        var command = new AddCustomerCommand(
            "FirstName",
            "LastName",
            "test@email.com",
            areaCode,
            "456",
            "7890",
            null,
            new List<AddressDto>());

        var handler = new AddCustomerCommandHandler(
            _customerRepositoryMock.Object,
            _unitOfWorkMock.Object);

        var result = await handler.Handle(command, default);

        result.IsFailure.Should().BeTrue();
        result.Error.Should().Be(DomainErrors.PhoneNumber.AreaCodeNotNumber);
    }

    [Theory]
    [InlineData("12")]
    [InlineData("1234")]
    public async Task Handle_Should_ReturnFailureResult_WhenAreaCodeIsInvalidLength(string areaCode)
    {
        var command = new AddCustomerCommand(
            "FirstName",
            "LastName",
            "test@email.com",
            areaCode,
            "456",
            "7890",
            null,
            new List<AddressDto>());

        var handler = new AddCustomerCommandHandler(
            _customerRepositoryMock.Object,
            _unitOfWorkMock.Object);

        var result = await handler.Handle(command, default);

        result.IsFailure.Should().BeTrue();
        result.Error.Should().Be(DomainErrors.PhoneNumber.AreaCodeInvalidLength);
    }

    [Fact]
    public async Task Handle_Should_ReturnFailureResult_WhenPrefixIsEmpty()
    {
        var command = new AddCustomerCommand(
            "FirstName",
            "LastName",
            "test@email.com",
            "123",
            string.Empty,
            "7890",
            null,
            new List<AddressDto>());

        var handler = new AddCustomerCommandHandler(
            _customerRepositoryMock.Object,
            _unitOfWorkMock.Object);

        var result = await handler.Handle(command, default);

        result.IsFailure.Should().BeTrue();
        result.Error.Should().Be(DomainErrors.PhoneNumber.PrefixEmpty);
    }


    [Theory]
    [InlineData("ABC")]
    [InlineData("!23")]
    public async Task Handle_Should_ReturnFailureResult_WhenPrefixIsNotANumber(string prefix)
    {
        var command = new AddCustomerCommand(
            "FirstName",
            "LastName",
            "test@email.com",
            "123",
            prefix,
            "7890",
            null,
            new List<AddressDto>());

        var handler = new AddCustomerCommandHandler(
            _customerRepositoryMock.Object,
            _unitOfWorkMock.Object);

        var result = await handler.Handle(command, default);

        result.IsFailure.Should().BeTrue();
        result.Error.Should().Be(DomainErrors.PhoneNumber.PrefixNotNumber);
    }

    [Theory]
    [InlineData("45")]
    [InlineData("4567")]
    public async Task Handle_Should_ReturnFailureResult_WhenPrefixIsInvalidLength(string prefix)
    {
        var command = new AddCustomerCommand(
            "FirstName",
            "LastName",
            "test@email.com",
            "123",
            prefix,
            "7890",
            null,
            new List<AddressDto>());

        var handler = new AddCustomerCommandHandler(
            _customerRepositoryMock.Object,
            _unitOfWorkMock.Object);

        var result = await handler.Handle(command, default);

        result.IsFailure.Should().BeTrue();
        result.Error.Should().Be(DomainErrors.PhoneNumber.PrefixInvalidLength);
    }

    [Fact]
    public async Task Handle_Should_ReturnFailureResult_WhenLineNumberIsEmpty()
    {
        var command = new AddCustomerCommand(
            "FirstName",
            "LastName",
            "test@email.com",
            "123",
            "456",
            string.Empty,
            null,
            new List<AddressDto>());

        var handler = new AddCustomerCommandHandler(
            _customerRepositoryMock.Object,
            _unitOfWorkMock.Object);

        var result = await handler.Handle(command, default);

        result.IsFailure.Should().BeTrue();
        result.Error.Should().Be(DomainErrors.PhoneNumber.LineNumberEmpty);
    }

    [Theory]
    [InlineData("ABC")]
    [InlineData("!23")]
    public async Task Handle_Should_ReturnFailureResult_WhenLineNumberIsNotANumber(string lineNumber)
    {
        var command = new AddCustomerCommand(
            "FirstName",
            "LastName",
            "test@email.com",
            "123",
            "456",
            lineNumber,
            null,
            new List<AddressDto>());

        var handler = new AddCustomerCommandHandler(
            _customerRepositoryMock.Object,
            _unitOfWorkMock.Object);

        var result = await handler.Handle(command, default);

        result.IsFailure.Should().BeTrue();
        result.Error.Should().Be(DomainErrors.PhoneNumber.LineNumberNotNumber);
    }

    [Theory]
    [InlineData("789")]
    [InlineData("78901")]
    public async Task Handle_Should_ReturnFailureResult_WhenLineNumberIsInvalidLength(string lineNumber)
    {
        var command = new AddCustomerCommand(
            "FirstName",
            "LastName",
            "test@email.com",
            "123",
            "456",
            lineNumber,
            null,
            new List<AddressDto>());

        var handler = new AddCustomerCommandHandler(
            _customerRepositoryMock.Object,
            _unitOfWorkMock.Object);

        var result = await handler.Handle(command, default);

        result.IsFailure.Should().BeTrue();
        result.Error.Should().Be(DomainErrors.PhoneNumber.LineNumberInvalidLength);
    }

    [Fact]
    public async Task Handle_Should_ReturnFailureResult_WhenAddressHasInvalidZipCode()
    {
        var command = new AddCustomerCommand(
            "FirstName",
            "LastName",
            "test@email.com",
            "123",
            "456",
            "7890",
            null,
            new List<AddressDto>
            {
                new AddressDto(
                    "Street",
                    "City",
                    "OH",
                    "abcde",
                    "Country",
                    "Home")
            });

        var handler = new AddCustomerCommandHandler(
            _customerRepositoryMock.Object,
            _unitOfWorkMock.Object);

        var result = await handler.Handle(command, default);

        result.IsFailure.Should().BeTrue();
        result.Error.Should().Be(DomainErrors.ZipCode.Invalid);
    }

    [Fact]
    public async Task Handle_Should_ReturnFailureResult_WhenAddressHasInvalidAddressType()
    {
        var command = new AddCustomerCommand(
            "FirstName",
            "LastName",
            "test@email.com",
            "123",
            "456",
            "7890",
            null,
            new List<AddressDto>
            {
                new AddressDto(
                    "Street",
                    "City",
                    "OH",
                    "12345",
                    "Country",
                    "Bad")
            });

        var handler = new AddCustomerCommandHandler(
            _customerRepositoryMock.Object,
            _unitOfWorkMock.Object);

        var result = await handler.Handle(command, default);

        result.IsFailure.Should().BeTrue();
        result.Error.Should().Be(DomainErrors.AddressType.Invalid);
    }

    [Fact]
    public async Task Handle_Should_ReturnFailureResult_WhenAddressHasInvalidState()
    {
        var command = new AddCustomerCommand(
            "FirstName",
            "LastName",
            "test@email.com",
            "123",
            "456",
            "7890",
            null,
            new List<AddressDto>
            {
                new AddressDto(
                    "Street",
                    "City",
                    "bad",
                    "12345",
                    "Country",
                    "Home")
            });

        var handler = new AddCustomerCommandHandler(
            _customerRepositoryMock.Object,
            _unitOfWorkMock.Object);

        var result = await handler.Handle(command, default);

        result.IsFailure.Should().BeTrue();
        result.Error.Should().Be(DomainErrors.State.Invalid);
    }

    [Fact]
    public async Task Handle_Should_ReturnSuccessResult_WhenAllRequestDataIsValid()
    {
        var command = new AddCustomerCommand(
            "FirstName",
            "LastName",
            "test@email.com",
            "123",
            "456",
            "7890",
            null,
            new List<AddressDto>
            {
                new AddressDto(
                    "Street",
                    "City",
                    "OH",
                    "12345",
                    "Country",
                    "Home")
            });

        var handler = new AddCustomerCommandHandler(
            _customerRepositoryMock.Object,
            _unitOfWorkMock.Object);

        var result = await handler.Handle(command, default);

        result.IsSuccess.Should().BeTrue();
    }

    [Fact]
    public async Task Handle_Should_CallAddOnRepository_WhenAllRequestDataIsValid()
    {
        var command = new AddCustomerCommand(
            "FirstName",
            "LastName",
            "test@email.com",
            "123",
            "456",
            "7890",
            null,
            new List<AddressDto>
            {
                new AddressDto(
                    "Street",
                    "City",
                    "OH",
                    "12345",
                    "Country",
                    "Home")
            });

        var handler = new AddCustomerCommandHandler(
            _customerRepositoryMock.Object,
            _unitOfWorkMock.Object);

        _ = await handler.Handle(command, default);

        _customerRepositoryMock.Verify(
            x => x.Add(It.IsAny<Customer>()),
            Times.Once);
    }

    [Fact]
    public async Task Handle_Should_PublishCustomerCreated_WhenAllRequestDataIsValid()
    {
        var command = new AddCustomerCommand(
            "FirstName",
            "LastName",
            "test@email.com",
            "123",
            "456",
            "7890",
            null,
            new List<AddressDto>
            {
                new AddressDto(
                    "Street",
                    "City",
                    "OH",
                    "12345",
                    "Country",
                    "Home")
            });

        var handler = new AddCustomerCommandHandler(
            _customerRepositoryMock.Object,
            _unitOfWorkMock.Object);

        _ = await handler.Handle(command, default);

        _unitOfWorkMock.Verify(
            x => x.SaveChangesAsync(It.IsAny<CancellationToken>()),
            Times.Once);
    }

    [Fact]
    public async Task Handle_ShouldNot_CallAddOnRepository_WhenRequestDataIsInvalid()
    {
        var command = new AddCustomerCommand(
            string.Empty,
            "LastName",
            "test@email.com",
            "123",
            "456",
            "7890",
            null,
            new List<AddressDto>
            {
                new AddressDto(
                    "Street",
                    "City",
                    "OH",
                    "12345",
                    "Country",
                    "Home")
            });

        var handler = new AddCustomerCommandHandler(
            _customerRepositoryMock.Object,
            _unitOfWorkMock.Object);

        _ = await handler.Handle(command, default);

        _customerRepositoryMock.Verify(
            x => x.Add(It.IsAny<Customer>()),
            Times.Never);
    }
}
