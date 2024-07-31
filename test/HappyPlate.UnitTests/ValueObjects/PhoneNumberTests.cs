using HappyPlate.Domain.Shared;
using HappyPlate.Domain.ValueObjects;

namespace HappyPlate.UnitTests.ValueObjects;

public class PhoneNumberTests
{
    [Theory]
    [InlineData("123", "456", "7890", null)]
    [InlineData("123", "456", "7890", "1111")]
    void Create_Should_ReturnSuccessResult_WhenPhoneNumberIsValid(
        string areaCode,
        string prefix,
        string lineNumber,
        string? extension)
    {
        Result<PhoneNumber> result = PhoneNumber.Create(areaCode, prefix, lineNumber, extension);

        result.IsSuccess.Should().BeTrue();
    }

    [Fact]
    void Create_Should_ReturnFailureResult_WhenAreaCodeIsEmpty()
    {
        Result<PhoneNumber> result = PhoneNumber.Create("", "456", "7890", null);

        result.IsFailure.Should().BeTrue();
        result.Error.Should().Be(DomainErrors.PhoneNumber.AreaCodeEmpty);
    }

    [Theory]
    [InlineData("ABC", "456", "7890", null)]
    [InlineData("!23", "456", "7890", null)]
    void Create_Should_ReturnFailureResult_WhenAreaCodeIsNotANumber(
        string areaCode,
        string prefix,
        string lineNumber,
        string? extension)
    {
        Result<PhoneNumber> result = PhoneNumber.Create(areaCode, prefix, lineNumber, extension);

        result.IsFailure.Should().BeTrue();
        result.Error.Should().Be(DomainErrors.PhoneNumber.AreaCodeNotNumber);
    }


    [Theory]
    [InlineData("12", "456", "7890", null)]
    [InlineData("1234", "456", "7890", null)]
    void Create_Should_ReturnFailureResult_WhenAreaCodeIsIncorrectLength(
        string areaCode,
        string prefix,
        string lineNumber,
        string? extension)
    {
        Result<PhoneNumber> result = PhoneNumber.Create(areaCode, prefix, lineNumber, extension);

        result.IsFailure.Should().BeTrue();
        result.Error.Should().Be(DomainErrors.PhoneNumber.AreaCodeInvalidLength);
    }


    [Fact]
    void Create_Should_ReturnFailureResult_WhenPrefixIsEmpty()
    {
        Result<PhoneNumber> result = PhoneNumber.Create("123", "", "7890", null);

        result.IsFailure.Should().BeTrue();
        result.Error.Should().Be(DomainErrors.PhoneNumber.PrefixEmpty);
    }

    [Theory]
    [InlineData("123", "ABC", "7890", null)]
    [InlineData("123", "@BC", "7890", null)]
    void Create_Should_ReturnFailureResult_WhenPrefixIsNotANumber(
        string Prefix,
        string prefix,
        string lineNumber,
        string? extension)
    {
        Result<PhoneNumber> result = PhoneNumber.Create(Prefix, prefix, lineNumber, extension);

        result.IsFailure.Should().BeTrue();
        result.Error.Should().Be(DomainErrors.PhoneNumber.PrefixNotNumber);
    }

    [Theory]
    [InlineData("123", "45", "7890", null)]
    [InlineData("123", "4567", "7890", null)]
    void Create_Should_ReturnFailureResult_WhenPrefixIsIncorrectLength(
        string Prefix,
        string prefix,
        string lineNumber,
        string? extension)
    {
        Result<PhoneNumber> result = PhoneNumber.Create(Prefix, prefix, lineNumber, extension);

        result.IsFailure.Should().BeTrue();
        result.Error.Should().Be(DomainErrors.PhoneNumber.PrefixInvalidLength);
    }

    [Fact]
    void Create_Should_ReturnFailureResult_WhenLineNumberIsEmpty()
    {
        Result<PhoneNumber> result = PhoneNumber.Create("123", "456", "", null);

        result.IsFailure.Should().BeTrue();
        result.Error.Should().Be(DomainErrors.PhoneNumber.LineNumberEmpty);
    }

    [Theory]
    [InlineData("123", "456", "ABCD", null)]
    [InlineData("123", "456", "7&90", null)]
    void Create_Should_ReturnFailureResult_WhenLineNumberIsNotANumber(
        string LineNumber,
        string prefix,
        string lineNumber,
        string? extension)
    {
        Result<PhoneNumber> result = PhoneNumber.Create(LineNumber, prefix, lineNumber, extension);

        result.IsFailure.Should().BeTrue();
        result.Error.Should().Be(DomainErrors.PhoneNumber.LineNumberNotNumber);
    }


    [Theory]
    [InlineData("123", "456", "789", null)]
    [InlineData("123", "456", "78901", null)]
    void Create_Should_ReturnFailureResult_WhenLineNumberIsIncorrectLength(
        string LineNumber,
        string prefix,
        string lineNumber,
        string? extension)
    {
        Result<PhoneNumber> result = PhoneNumber.Create(LineNumber, prefix, lineNumber, extension);

        result.IsFailure.Should().BeTrue();
        result.Error.Should().Be(DomainErrors.PhoneNumber.LineNumberInvalidLength);
    }

    [Theory]
    [InlineData("123", "456", "7890", null, "(123) 456-7890")]
    [InlineData("123", "456", "7890", "1111", "(123) 456-7890 ext. 1111")]
    void Create_Should_HaveCorrectlyFormatedNumber_WhenPhoneNumberIsValid(
        string areaCode,
        string prefix,
        string lineNumber,
        string? extension,
        string expected)
    {
        Result<PhoneNumber> result = PhoneNumber.Create(areaCode, prefix, lineNumber, extension);

        result.Value.Number.Should().Be(expected);
    }

}
