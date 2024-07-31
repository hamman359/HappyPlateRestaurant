using HappyPlate.Domain.Shared;

using HappyPlate.Domain.ValueObjects;

namespace HappyPlate.UnitTests.ValueObjects;

public class LastNameTests
{
    [Fact]
    void Create_Should_ReturnSuccess_WhenLastNameIsValid()
    {
        var name = "Last";

        Result<LastName> result = LastName.Create(name);

        result.IsSuccess.Should().BeTrue();
        result.Value.Value.Should().Be(name);
    }

    [Fact]
    void Create_Should_ReturnFailure_WhenLastNameIsEmpty()
    {
        var name = string.Empty;

        Result<LastName> result = LastName.Create(name);

        result.IsFailure.Should().BeTrue();
        result.Error.Should().Be(DomainErrors.LastName.Empty);
    }

    [Fact]
    void Create_Should_ReturnFailure_WhenLastNameTooLong()
    {
        var name = new string('a', 51);

        Result<LastName> result = LastName.Create(name);

        result.IsFailure.Should().BeTrue();
        result.Error.Should().Be(DomainErrors.LastName.TooLong);
    }
}
