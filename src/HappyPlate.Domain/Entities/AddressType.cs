using HappyPlate.Domain.Primatives;

namespace HappyPlate.Domain.Entities;

public sealed class AddressType : Enumeration<AddressType>
{
    public static readonly AddressType Home = new(1, "Home");
    public static readonly AddressType Work = new(2, "Work");
    public static readonly AddressType Mailing = new(3, "Mailing");

    AddressType(int id, string name)
        : base(id, name)
    {
    }
}
