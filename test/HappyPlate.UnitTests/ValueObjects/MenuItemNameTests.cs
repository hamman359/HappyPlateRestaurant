using HappyPlate.Domain.Shared;

using HappyPlate.Domain.ValueObjects;

namespace HappyPlate.UnitTests.ValueObjects;

public class MenuItemNameTests
{
    [Fact]
    void Create_Should_ReturnSuccess_WhenMenuItemNameIsValid()
    {
        var name = "MenuItem";

        Result<MenuItemName> result = MenuItemName.Create(name);

        result.IsSuccess.Should().BeTrue();
        result.Value.Value.Should().Be(name);
    }

    [Fact]
    void Create_Should_ReturnFailure_WhenMenuItemNameIsEmpty()
    {
        var name = string.Empty;

        Result<MenuItemName> result = MenuItemName.Create(name);

        result.IsFailure.Should().BeTrue();
        result.Error.Should().Be(DomainErrors.MenuItemName.Empty);
    }

    [Fact]
    void Create_Should_ReturnFailure_WhenMenuItemNameTooLong()
    {
        var name = new string('a', 51);

        Result<MenuItemName> result = MenuItemName.Create(name);

        result.IsFailure.Should().BeTrue();
        result.Error.Should().Be(DomainErrors.MenuItemName.TooLong);
    }
}
