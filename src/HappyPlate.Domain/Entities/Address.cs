using HappyPlate.Domain.Enums;
using HappyPlate.Domain.Primatives;
using HappyPlate.Domain.ValueObjects;

namespace HappyPlate.Domain.Entities;

public sealed class Address : Entity
{
    Address(
        string street,
        string city,
        State state,
        ZipCode zipCode,
        string country,
        AddressType type)
    {
        Street = street;
        City = city;
        State = state;
        ZipCode = zipCode;
        Country = country;
        Type = type;
    }

    Address()
    {
    }

    public Guid CustomerId { get; private set; }

    public string Street { get; private set; }

    public string City { get; private set; }

    public State State { get; private set; }

    public ZipCode ZipCode { get; private set; }

    public string Country { get; private set; }

    public AddressType Type { get; private set; }

    public static Address Create(
        string street,
        string city,
        State state,
        ZipCode zipCode,
        string country,
        AddressType type)
    {
        return new(
            street,
            city,
            state,
            zipCode,
            country,
            type);
    }
}
