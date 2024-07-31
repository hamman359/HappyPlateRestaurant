using HappyPlate.Domain.Shared;

using HappyPlate.Domain.ValueObjects;

namespace HappyPlate.UnitTests.ValueObjects;

public class EmailTests
{
    [Fact]
    void Create_Should_ReturnSuccessResult_WhenEmailIsValid()
    {
        var email = "Test@email.com";

        Result<Email> result = Email.Create(email);

        result.IsSuccess.Should().BeTrue();
        result.Value.Value.Should().Be(email);
    }

    [Fact]
    void Create_Should_ReturnFailureResult_WhenEmailIsInvalidlyFormatted()
    {
        var email = "Testemail.com";

        Result<Email> result = Email.Create(email);

        result.IsFailure.Should().BeTrue();
        result.Error.Should().Be(DomainErrors.Email.InvalidFormat);
    }

    [Fact]
    void Create_Should_ReturnFailureResult_WhenEmailIsTooLong()
    {
        var email = "TestTestTestTestTestTestTestTestTestTestTestTestTestTestTestTestTestTestTestTestTestTestTestTestTestTestTestTestTestTestTestTestTestTestTestTestTestTestTestTestTestTestTestTestTestTestTestTestTestTestTestTestTestTestTestTestTestTestTestTestTestTest@email.com";

        Result<Email> result = Email.Create(email);

        result.IsFailure.Should().BeTrue();
        result.Error.Should().Be(DomainErrors.Email.TooLong);
    }

    [Fact]
    void Create_Should_ReturnFailureResult_WhenEmailIsEmpty()
    {
        var email = "";

        Result<Email> result = Email.Create(email);

        result.IsFailure.Should().BeTrue();
        result.Error.Should().Be(DomainErrors.Email.Empty);
    }
}
