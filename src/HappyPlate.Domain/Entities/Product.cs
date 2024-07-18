using HappyPlate.Domain.Primatives;

namespace HappyPlate.Domain.Entities;

public sealed class Product : Entity
{
    Product(
        string name,
        string description,
        float price,
        Category category)
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

    public float Price { get; set; }

    public Category Category { get; set; }

    public DateTime CreatedOnUtc { get; set; }

    public DateTime? ModifiedOnUtc { get; set; }

    public static Product Create(
        string name,
        string description,
        float price,
        Category category)
    {
        return new Product(name, description, price, category);
    }

}
