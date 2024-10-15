using uBee.Domain.Core.Primitives;

namespace uBee.Domain.Errors
{
    public static class DomainError
    {
        public static class General
        {
            public static Error UnProcessableRequest => new Error(
                "General.UnProcessableRequest",
                "The server could not process the request.");

            public static Error ServerError => new Error(
                "General.ServerError",
                "The server encountered an unrecoverable error.");

            public static Error AlreadyDeleted => new Error(
                "General.AlreadyDeleted",
                "The entity has already been deleted.");
        }

        public static class Authentication
        {
            public static Error InvalidEmailOrPassword => new Error(
                "Authentication.InvalidEmailOrPassword",
                "The specified email or password are incorrect.");
        }

        public static class Email
        {
            public static Error NullOrEmpty => new Error(
                "Email.NullOrEmpty",
                "The email is required.");

            public static Error LongerThanAllowed => new Error(
                "Email.LongerThanAllowed",
                "The email is longer than allowed.");

            public static Error InvalidFormat => new Error(
                "Email.InvalidFormat",
                "The email format is invalid.");
        }

        public static class Password
        {
            public static Error NullOrEmpty => new Error(
                "Password.NullOrEmpty",
                "The password is required.");

            public static Error TooShort => new Error(
                "Password.TooShort",
                "The password is too short.");

            public static Error MissingUppercaseLetter => new Error(
                "Password.MissingUppercaseLetter",
                "The password requires at least one uppercase letter.");

            public static Error MissingLowercaseLetter => new Error(
                "Password.MissingLowercaseLetter",
                "The password requires at least one lowercase letter.");

            public static Error MissingDigit => new Error(
                "Password.MissingDigit",
                "The password requires at least one digit.");

            public static Error MissingNonAlphaNumeric => new Error(
                "Password.MissingNonAlphaNumeric",
                "The password requires at least one non-alphanumeric.");
        }

        public static class User
        {
            public static Error NotFound => new Error(
                "User.NotFound",
                "The user with the specified identifier was not found.");

            public static Error InvalidPermissions => new Error(
                "User.InvalidPermissions",
                "The current user does not have the permissions to perform that operation.");

            public static Error DuplicateEmail => new Error(
                "User.DuplicateEmail",
                "The specified email is already in use.");

            public static Error CannotChangePassword => new Error(
                "User.CannotChangePassword",
                "The password cannot be changed to the specified password.");

            public static Error NameIsRequired = new Error(
                "User.NameIsRequired",
                "The user name is required.");

            public static Error SurnameIsRequired = new Error(
                "User.SurnameIsRequired",
                "The user surname is required.");
        }

        public static class Hive
        {
            public static Error CannotMarkInUse => new Error(
                "Hive.CannotMarkInUse",
                "Only available hives can be marked as in use.");

            public static Error CannotMarkAvailable => new Error(
                "Hive.CannotMarkAvailable",
                "Decommissioned hives cannot be made available again.");

            public static Error CannotMarkUnderMaintenance => new Error(
                "Hive.CannotMarkUnderMaintenance",
                "In-use hives cannot be marked as under maintenance.");

            public static Error CannotDecommission => new Error(
                "Hive.CannotDecommission",
                "In-use hives cannot be decommissioned.");

            public static Error InvalidHive => new Error(
                "Hive.InvalidHive",
                "The specified hive is invalid.");
        }

        public static class BeeContract
        {
            public static Error InvalidStatusChange => new Error(
                "BeeContract.InvalidStatusChange",
                "Cannot change the status of a contract that is completed or cancelled.");

            public static Error InvalidPrice => new Error(
                "BeeContract.InvalidPrice",
                "The price must be greater than zero.");

            public static Error InvalidUser => new Error(
                "BeeContract.InvalidUser",
                "The user for this contract is invalid.");
        }

        public static class ContractedHive
        {
            public static Error InvalidHive => new Error(
                "ContractedHive.InvalidHive",
                "The hive ID must not be empty.");

            public static Error InvalidContract => new Error(
                "ContractedHive.InvalidContract",
                "The contract ID must not be empty.");
        }

    }
}
