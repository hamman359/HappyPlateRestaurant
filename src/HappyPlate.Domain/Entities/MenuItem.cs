using HappyPlate.Domain.Primatives;
using HappyPlate.Domain.ValueObjects;

namespace HappyPlate.Domain.Entities;

public sealed class MenuItem : Entity
{
    MenuItem(
        Guid id,
        string name,
        string description,
        Price price,
        string category,
        string image)
        : base(id)
    {
        Image = image;
        Name = name;
        Description = description;
        Price = price;
        Category = category;
        CreatedOnUtc = DateTime.UtcNow;
        ModifiedOnUtc = DateTime.UtcNow;
    }

    public string Name { get; private set; }

    public string Description { get; private set; }

    public Price Price { get; private set; }

    public string Category { get; private set; }

    public string Image { get; private set; }

    public DateTime CreatedOnUtc { get; set; }

    public DateTime? ModifiedOnUtc { get; set; }

    public static MenuItem Create(
        string name,
        string description,
        Price price,
        string category,
        string image)
    {
        return new MenuItem(
            Guid.NewGuid(),
            name,
            description,
            price,
            category,
            image);
    }

}
