using HappyPlate.Domain.Shared;

using HappyPlate.Domain.ValueObjects;

namespace HappyPlate.UnitTests.ValueObjects;

public class ZipCodeTests
{
    [Theory]
    [InlineData("12345")]
    [InlineData("12345-6789")]
    [InlineData("12345 6789")]
    [InlineData("123456789")]
    void Create_Should_ReturnSuccessResult_WhenZipCodeIsValid(string zip)
    {
        Result<ZipCode> result = ZipCode.Create(zip);

        result.IsSuccess.Should().BeTrue();
    }

    [Theory]
    [InlineData("1234")]
    [InlineData("123456")]
    [InlineData("1234-6789")]
    [InlineData("1234 6789")]
    [InlineData("12346789")]
    [InlineData("12345 678")]
    [InlineData("12345 67890")]
    [InlineData("12345-67890")]
    [InlineData("123456 6789")]
    [InlineData("123456-6789")]
    [InlineData("1234567890")]
    void Create_Should_ReturnFailureResult_WhenZipCodeIsInvalid(string zip)
    {
        Result<ZipCode> result = ZipCode.Create(zip);

        result.IsFailure.Should().BeTrue();
    }
}
