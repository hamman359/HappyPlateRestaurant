using HappyPlate.Domain.Shared;

using HappyPlate.Domain.ValueObjects;

namespace HappyPlate.UnitTests.ValueObjects;

public class FirstNameTests
{
    [Fact]
    void Create_Should_ReturnSuccess_WhenFirstNameIsValid()
    {
        var name = "First";

        Result<FirstName> result = FirstName.Create(name);

        result.IsSuccess.Should().BeTrue();
        result.Value.Value.Should().Be(name);
    }

    [Fact]
    void Create_Should_ReturnFailure_WhenFirstNameIsEmpty()
    {
        var name = string.Empty;

        Result<FirstName> result = FirstName.Create(name);

        result.IsFailure.Should().BeTrue();
        result.Error.Should().Be(DomainErrors.FirstName.Empty);
    }

    [Fact]
    void Create_Should_ReturnFailure_WhenFirstNameTooLong()
    {
        var name = new String('a', 51);

        Result<FirstName> result = FirstName.Create(name);

        result.IsFailure.Should().BeTrue();
        result.Error.Should().Be(DomainErrors.FirstName.TooLong);
    }
}
