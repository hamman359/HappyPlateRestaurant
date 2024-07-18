using HappyPlate.Domain.Primatives;
using HappyPlate.Domain.ValueObjects;

namespace HappyPlate.Domain.Entities;

public sealed class Product : Entity
{
    Product(
        Guid id,
        string name,
        string description,
        Price price,
        string category)
        : base(id)
    {
        Name = name;
        Description = description;
        Price = price;
        Category = category;
        CreatedOnUtc = DateTime.UtcNow;
        ModifiedOnUtc = DateTime.UtcNow;
    }

    Product()
    {
    }

    public string Name { get; set; }

    public string Description { get; set; }

    public Price Price { get; set; }

    public string Category { get; set; }

    public DateTime CreatedOnUtc { get; set; }

    public DateTime? ModifiedOnUtc { get; set; }

    public static Product Create(
        string name,
        string description,
        float price,
        string category)
    {
        return new Product(
            Guid.NewGuid(),
            name,
            description,
            Price.Create(price),
            category);
    }

}
