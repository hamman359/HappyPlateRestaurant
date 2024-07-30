using HappyPlate.Domain.Primatives;

namespace HappyPlate.Domain.Entities;

public sealed class Address : Entity
{
    Address(
        string street,
        string city,
        string state,
        string zipCode,
        string country,
        string type)
    {
        Street = street;
        City = city;
        State = state;
        ZipCode = zipCode;
        Country = country;
        Type = type;
    }

    public string Street { get; private set; }

    public string City { get; private set; }

    public string State { get; private set; }

    public string ZipCode { get; private set; }

    public string Country { get; private set; }

    public string Type { get; private set; }

    public static Address Create(
        string street,
        string city,
        string state,
        string zipCode,
        string country,
        string type)
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
