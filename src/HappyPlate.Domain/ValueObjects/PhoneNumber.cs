using HappyPlate.Domain.Errors;
using HappyPlate.Domain.Primatives;
using HappyPlate.Domain.Shared;

namespace HappyPlate.Domain.ValueObjects;

public sealed class PhoneNumber : ValueObject
{
    const int AreaCodeLength = 3;
    const int PrefixLength = 3;
    const int LineNumberLength = 4;

    PhoneNumber(
        string areaCode,
        string prefix,
        string lineNumber,
        string extension)
    {
        AreaCode = areaCode;
        Prefix = prefix;
        LineNumber = lineNumber;
        Extension = extension;
    }

    public string AreaCode { get; private set; }

    public string Prefix { get; private set; }

    public string LineNumber { get; private set; }

    public string Extension { get; private set; }

    public string Number =>
        string.IsNullOrEmpty(Extension)
            ? $"({AreaCode}) {Prefix}-{LineNumber}"
            : $"({AreaCode}) {Prefix}-{LineNumber} ext. {Extension}";

    public static Result<PhoneNumber> Create(
        string AreaCode,
        string Prefix,
        string LineNumber,
        string? Extension)
    {
        if(string.IsNullOrWhiteSpace(AreaCode))
        {
            return Result.Failure<PhoneNumber>(DomainErrors.PhoneNumber.AreaCodeEmpty);
        }

        if(!AreaCode.All(char.IsAsciiDigit))
        {
            return Result.Failure<PhoneNumber>(DomainErrors.PhoneNumber.AreaCodeNotNumber);
        }

        if(AreaCode.Length != AreaCodeLength)
        {
            return Result.Failure<PhoneNumber>(DomainErrors.PhoneNumber.AreaCodeInvalidLength);
        }

        if(string.IsNullOrWhiteSpace(Prefix))
        {
            return Result.Failure<PhoneNumber>(DomainErrors.PhoneNumber.PrefixEmpty);
        }

        if(!Prefix.All(char.IsAsciiDigit))
        {
            return Result.Failure<PhoneNumber>(DomainErrors.PhoneNumber.PrefixNotNumber);
        }

        if(Prefix.Length != PrefixLength)
        {
            return Result.Failure<PhoneNumber>(DomainErrors.PhoneNumber.PrefixInvalidLength);
        }

        if(string.IsNullOrWhiteSpace(LineNumber))
        {
            return Result.Failure<PhoneNumber>(DomainErrors.PhoneNumber.LineNumberEmpty);
        }

        if(!LineNumber.All(char.IsAsciiDigit))
        {
            return Result.Failure<PhoneNumber>(DomainErrors.PhoneNumber.LineNumberNotNumber);
        }

        if(LineNumber.Length != LineNumberLength)
        {
            return Result.Failure<PhoneNumber>(DomainErrors.PhoneNumber.LineNumberInvalidLength);
        }

        return new PhoneNumber(AreaCode, Prefix, LineNumber, Extension ?? string.Empty);
    }

    public override IEnumerable<object> GetAtomicValues()
    {
        yield return AreaCode;
        yield return Prefix;
        yield return LineNumber;
        yield return Extension;
    }
}
