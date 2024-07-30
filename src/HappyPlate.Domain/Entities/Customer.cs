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
        Address address)
    {
        FirstName = firstName;
        LastName = lastName;
        Email = email;
        PhoneNumber = phoneNumber;
        Address = address;
    }

    public FirstName FirstName { get; private set; }

    public LastName LastName { get; private set; }

    public Email Email { get; private set; }

    public PhoneNumber PhoneNumber { get; private set; }

    public Address Address { get; private set; }

    public static Customer Create(
        FirstName firstName,
        LastName lastName,
        Email email,
        PhoneNumber phoneNumber,
        Address address)
    {
        return new(
            firstName,
            lastName,
            email,
            phoneNumber,
            address);
    }
}
