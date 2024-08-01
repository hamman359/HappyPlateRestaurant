using HappyPlate.Domain.Primatives;
using HappyPlate.Domain.ValueObjects;

namespace HappyPlate.Domain.Entities;

public sealed class Customer : AggregateRoot
{
    private readonly List<Address> _addresses = new();

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
        _addresses.AddRange(addresses);
    }

    Customer()
    {
    }

    public FirstName FirstName { get; private set; }

    public LastName LastName { get; private set; }

    public Email Email { get; private set; }

    public PhoneNumber PhoneNumber { get; private set; }

    public IReadOnlyCollection<Address> Addresses => _addresses;

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
