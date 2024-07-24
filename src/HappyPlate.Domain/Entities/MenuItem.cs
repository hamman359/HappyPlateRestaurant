using HappyPlate.Domain.Primatives;
using HappyPlate.Domain.ValueObjects;

namespace HappyPlate.Domain.Entities;

public sealed class MenuItem : Entity
{
    MenuItem(
        Guid id,
        MenuItemName name,
        string description,
        Price price,
        string category,
        string image,
        bool isAvailable)
        : base(id)
    {
        Image = image;
        Name = name;
        Description = description;
        Price = price;
        Category = category;
        IsAvailable = isAvailable;
        CreatedOnUtc = DateTime.UtcNow;
        ModifiedOnUtc = DateTime.UtcNow;
    }

    public MenuItemName Name { get; private set; }

    public string Description { get; private set; }

    public Price Price { get; private set; }

    public string Category { get; private set; }

    public string Image { get; private set; }

    public bool IsAvailable { get; private set; }

    public DateTime CreatedOnUtc { get; set; }

    public DateTime? ModifiedOnUtc { get; set; }

    public static MenuItem Create(
        MenuItemName name,
        string description,
        Price price,
        string category,
        string image,
        bool isAvailable)
    {
        return new MenuItem(
            Guid.NewGuid(),
            name,
            description,
            price,
            category,
            image,
            isAvailable);
    }

    public void ToggleAvailability()
    {
        IsAvailable = !IsAvailable;
    }
}
