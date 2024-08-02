using HappyPlate.Domain.Shared;

namespace HappyPlate.Domain.Errors;

public static class DomainErrors
{
    public static class AddressType
    {
        public static readonly Error Invalid = new(
            "AddressType.Invalid",
            "AddressType is not a valid value");
    }

    public static class Member
    {
        public static readonly Error EmailAlreadyInUse = new(
            "Member.EmailAlreadyInUse",
            "The specified email is already in use");

        public static readonly Func<Guid, Error> NotFound = id => new Error(
            "Member.NotFound",
            $"The member with the identifier {id} was not found.");

        public static readonly Error InvalidCredentials = new(
            "Member.InvalidCredentials",
            "The provided credentials are invalid");
    }

    public static class Gathering
    {
        public static readonly Func<Guid, Error> NotFound = id => new Error(
            "Gathering.NotFound",
            $"The gathering with the identifier {id} was not found.");

        public static readonly Error InvitingCreator = new(
            "Gathering.InvitingCreator",
            "Can't send invitation to the gathering creator");

        public static readonly Error AlreadyPassed = new(
            "Gathering.AlreadyPassed",
            "Can't send invitation for gathering in the past");

        public static readonly Error Expired = new(
            "Gathering.Expired",
            "Can't accept invitation for expired gathering");
    }

    public static class Email
    {
        public static readonly Error Empty = new(
            "Email.Empty",
            "Email is empty");

        public static readonly Error TooLong = new(
            "Email.TooLong",
            "Email is too long");

        public static readonly Error InvalidFormat = new(
            "Email.InvalidFormat",
            "Email format is invalid");
    }

    public static class FirstName
    {
        public static readonly Error Empty = new(
            "FirstName.Empty",
            "First name is empty");

        public static readonly Error TooLong = new(
            "LastName.TooLong",
            "FirstName name is too long");
    }

    public static class LastName
    {
        public static readonly Error Empty = new(
            "LastName.Empty",
            "Last name is empty");

        public static readonly Error TooLong = new(
            "LastName.TooLong",
            "Last name is too long");
    }

    public static class Price
    {
        public static readonly Error Negative = new(
            "Price.Negative",
            "Price is a negative amount");
    }

    public static class MenuItem
    {
        public static readonly Func<Guid, NotFoundError> NotFound = id => new NotFoundError(
            "MenuItem.NotFound",
            $"The Menu Item with Id {id} was not found");
    }

    public static class MenuItemName
    {
        public static readonly Error Empty = new(
            "MenuItemName.Empty",
            "Menu Item name is empty");

        public readonly static Error TooLong = new(
            "MenuItemName.TooLong",
            "MenuItem name is too long");

    }

    public static class PhoneNumber
    {
        public static readonly Error AreaCodeEmpty = new(
            "PhoneNumber.AreaCodeEmpty",
            "Phone Number Area Code is Empty");

        public static readonly Error PrefixEmpty = new(
            "PhoneNumber.PrefixEmpty",
            "Phone Number Prefix is Empty");

        public static readonly Error LineNumberEmpty = new(
            "PhoneNumber.LineNumberEmpty",
            "Phone Number Line Number is Empty");

        public static readonly Error AreaCodeNotNumber = new(
            "PhoneNumber.AreaCodeNotNumber",
            "Phone Number Area Code is not a valid number");

        public static readonly Error PrefixNotNumber = new(
            "PhoneNumber.PrefixNotNumber",
            "Phone Number Prefix is not a valid number");

        public static readonly Error LineNumberNotNumber = new(
            "PhoneNumber.LineNumberNotNumber",
            "Phone Number Line Number is not a valid number");

        public static readonly Error AreaCodeInvalidLength = new(
            "PhoneNumber.AreaCodeInvalidLength",
            "Phone Number Area Code must be exactly 3 digits long");

        public static readonly Error PrefixInvalidLength = new(
            "PhoneNumber.PrefixInvalidLength",
            "Phone Number Prefix must be exactly 3 digits long");

        public static readonly Error LineNumberInvalidLength = new(
            "PhoneNumber.LineNumberInvalidLength",
            "Phone Number Line Number must be exactly 4 digits long");
    }

    public static class ZipCode
    {
        public static readonly Error Invalid = new(
            "ZipCode.Invalid",
            "Zip Code is not valid");
    }

    public static class Address
    {
        public static readonly Error AddressTypeInvalid = new(
            "Address.AddressTypeInvalid",
            "Address Type is not a valid Address Type");

        public static readonly Error StateIsInvalid = new(
            "Address.StateInvalid",
            "State is not valid");
    }

    public static class State
    {
        public static readonly Error Invalid = new(
            "State.Invalid",
            "State is not a valid value");
    }

    public static class Customer
    {
        public static readonly Func<Guid, NotFoundError> NotFound = id => new NotFoundError(
            "Customer.NotFound",
            $"The Customer with Id {id} was not found");
    }
}