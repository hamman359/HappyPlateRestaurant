using HappyPlate.Domain.Primatives;
using HappyPlate.Domain.ValueObjects;

namespace HappyPlate.Domain.Entities;

public sealed class Customer : AggregateRoot
{
    Customer(
        FirstName firstName,
        LastName lastName,
        Email email,
        PhoneNumber phoneNumber,
        IList<Address> addresses)
    {
        FirstName = firstName;
        LastName = lastName;
        Email = email;
        PhoneNumber = phoneNumber;
        Addresses = addresses;
    }

    public FirstName FirstName { get; private set; }

    public LastName LastName { get; private set; }

    public Email Email { get; private set; }

    public PhoneNumber PhoneNumber { get; private set; }

    public IList<Address> Addresses { get; private set; }

    public static Customer Create(
        FirstName firstName,
        LastName lastName,
        Email email,
        PhoneNumber phoneNumber,
        IList<Address> addresses)
    {
        return new(
            firstName,
            lastName,
            email,
            phoneNumber,
            addresses);
    }
}
