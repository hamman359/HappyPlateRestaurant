using HappyPlate.Domain.Shared;

using HappyPlate.Domain.ValueObjects;

namespace HappyPlate.UnitTests.ValueObjects;

public class PriceTests
{
    [Fact]
    void Create_Should_ReturnFailureResult_WhenPriceIsNegative()
    {
        Result<Price> result = Price.Create(-1.0f);

        result.IsFailure.Should().BeTrue();
        result.Error.Should().Be(DomainErrors.Price.Negative);
    }

    [Fact]
    void Create_Should_ReturnSuccessResult_WhenPriceIsValid()
    {
        Result<Price> result = Price.Create(1.0f);

        result.IsSuccess.Should().BeTrue();
    }
}
