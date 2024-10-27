using uBee.Domain.Core.Primitives;

namespace uBee.Domain.Errors
{
    public static class DomainError
    {
        #region General
        public static class General
        {
            public static Error UnProcessableRequest => new Error(
                "General.UnProcessableRequest",
                "The request cannot be processed. Please verify the input data.");

            public static Error ServerError => new Error(
                "General.ServerError",
                "An internal server error occurred. Please try again later.");
        }

        #endregion

        #region Entities

        public static class User
        {
            public static Error NotFound => new Error(
                "User.NotFound",
                "The user with the specified identifier was not found.");

            public static Error EmailUnavailable => new Error(
                "User.EmailUnavailable",
                "The specified email is unavailable for use or is already in use.");

            public static Error DuplicateCpf => new Error(
                "User.DuplicateCpf",
                "The specified Cpf is already in use.");

            public static Error DuplicatePhone => new Error(
                "User.DuplicatePhone",
                "The specified number phone is already in use.");

            public static Error CannotChangePassword => new Error(
                "User.CannotChangePassword",
                "The password cannot be changed to the same current password.");

            public static Error NameIsRequired = new Error(
                "User.NameIsRequired",
                "The user name is required.");

            public static Error SurnameIsRequired = new Error(
                "User.SurnameIsRequired",
                "The user surname is required.");

            public static Error AlreadyDeleted => new Error(
                "User.AlreadyDeleted",
                "The user has already been deleted.");
        }

        public static class Location
        {
            public static Error InvalidAreaCode => new Error(
                "Location.InvalidAreaCode",
                "Invalid DDD. The provided DDD does not correspond to any valid region in Brazil. Please ensure the DDD is correct, e.g., '11' for São Paulo.");
        }

        public static class Hive
        {
            public static Error NotFound => new Error(
                "Hive.NotFound",
                "The hive with the specified identifier was not found.");

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
            public static Error NotFound => new Error(
                "BeeContract.NotFound",
                "The contract with the specified identifier was not found.");

            public static Error InvalidStatusChange => new Error(
                "BeeContract.InvalidStatusChange",
                "Cannot change the status of a contract that is completed or cancelled.");

            public static Error InvalidPrice => new Error(
                "BeeContract.InvalidPrice",
                "The price must be greater than zero.");
        }

        public static class ContractedHive
        {
            public static Error InvalidHive => new Error(
                "ContractedHive.InvalidHive",
                "The hive ID must not be empty.");
        }

        #endregion

        #region Value Objects

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
                "Invalid email format. Please ensure the email follows the format 'example@domain.com'.");
        }

        public static class Cpf
        {
            public static Error InvalidFormat => new Error(
                "Cpf.InvalidFormat",
                "Invalid CPF format. A valid CPF must contain exactly 11 digits, without punctuation, e.g., '12345678909'.");

            public static Error InvalidChecksum => new Error(
                "Cpf.InvalidChecksum",
                "The CPF digits are incorrect. Please ensure the CPF is valid and correctly entered.");
        }

        public static class Phone
        {
            public static Error InvalidFormat => new Error(
                "Phone.InvalidFormat",
                "Invalid phone format. A valid phone number must follow the format 'XX-XXXXXXXX' or 'XX-XXXXXXXXX', where 'XX' is the area code, e.g., '11-987654321'.");
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
                "The password must contain at least one uppercase letter.");

            public static Error MissingLowercaseLetter => new Error(
                "Password.MissingLowercaseLetter",
                "The password must contain at least one lowercase letter.");

            public static Error MissingDigit => new Error(
                "Password.MissingDigit",
                "The password must contain at least one digit.");

            public static Error MissingNonAlphaNumeric => new Error(
                "Password.MissingNonAlphaNumeric",
                "The password must contain at least one non-alphanumeric character.");

            public static Error InvalidCurrentPassword => new Error(
                "Password.InvalidPassword",
                "The specified current password is incorrect.");
        }

        #endregion

        #region Enumerators

        public static class UserRole
        {
            public static Error NotFound => new Error(
                "UserRole.NotFound",
                "The user role with the specified identifier was not found.");
        }

        #endregion

        #region Authentication
        public static class Authentication
        {
            public static Error InvalidEmailOrPassword => new Error(
                "Authentication.InvalidEmailOrPassword",
                "The specified email or password is incorrect.");

            public static Error AccountDeleted => new Error(
                "Authentication.AccountDeleted",
                "The account has been deleted. Please contact support for assistance.");

            public static Error EmailNotRegistered => new Error(
                "Authentication.EmailNotRegistered",
                "The specified email is not registered in the system.");
        }
        #endregion
    }
}
