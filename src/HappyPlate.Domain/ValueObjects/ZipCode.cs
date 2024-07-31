using System.Diagnostics.CodeAnalysis;
using System.Text.RegularExpressions;

using HappyPlate.Domain.Errors;
using HappyPlate.Domain.Primatives;
using HappyPlate.Domain.Shared;

namespace HappyPlate.Domain.ValueObjects;

public sealed class ZipCode : ValueObject
{
    [StringSyntax(StringSyntaxAttribute.Regex)]
    const string regexPattern = @"^\d{5}(?:[-\s]?\d{4})?$";

    public string Value { get; set; }

    ZipCode(string value)
    {
        Value = value;
    }

    public static Result<ZipCode> Create(string value)
    {
        var regex = new Regex(regexPattern);

        if(!regex.IsMatch(value))
        {
            return Result.Failure<ZipCode>(DomainErrors.ZipCode.Invalid);
        }

        return new ZipCode(value);
    }

    public override IEnumerable<object> GetAtomicValues()
    {
        yield return Value;
    }
}
