using HappyPlate.Domain.Errors;
using HappyPlate.Domain.Primatives;
using HappyPlate.Domain.Shared;

namespace HappyPlate.Domain.ValueObjects;

public sealed class Email : ValueObject
{
    public const int MaxLength = 255;

    private Email(string value) => Value = value;

    private Email()
    {
    }

    public string Value { get; private set; }

    public static Result<Email> Create(string email)
    {
        //return Result.Ensure(
        //    email,
        //    (e => !string.IsNullOrWhiteSpace(e), DomainErrors.Email.Empty),
        //    (e => e.Length <= MaxLength, DomainErrors.Email.TooLong),
        //    (e => e.Split('@').Length == 2, DomainErrors.Email.InvalidFormat))
        //    .Map(e => new Email(e));

        //        DomainErrors.Email.InvalidFormat)));
        //V2
        //return Result.Combine(
        //    Result.Ensure(
        //        email,
        //        e => !string.IsNullOrWhiteSpace(e),
        //        DomainErrors.Email.Empty),
        //    Result.Ensure(
        //        email,
        //        e => e.Length <= MaxLength,
        //        DomainErrors.Email.TooLong),
        //    Result.Ensure(
        //        email,
        //        e => e.Split('@').Length == 2,
        //        DomainErrors.Email.InvalidFormat))
        //    .Map(e => new Email(e));
        //V1
        return Result.Create(email)
            .Ensure(
                e => !string.IsNullOrWhiteSpace(e),
                DomainErrors.Email.Empty)
            .Ensure(
                e => e.Length <= MaxLength,
                DomainErrors.Email.TooLong)
            .Ensure(
                e => e.Split('@').Length == 2,
                DomainErrors.Email.InvalidFormat)
            .Map(e => new Email(e));
    }

    public override IEnumerable<object> GetAtomicValues()
    {
        yield return Value;
    }
}