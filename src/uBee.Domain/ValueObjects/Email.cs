using System.Text.RegularExpressions;
using uBee.Domain.Core.Primitives;
using uBee.Domain.Errors;
using uBee.Shared.Extensions;

namespace uBee.Domain.ValueObjects
{
    public sealed class Email : ValueObject
    {
        #region Constants

        public const int MaxLength = 256;
        private const string EmailRegexPattern = @"^(?!\.)(""([^""\r\\]|\\[""\r\\])*""|([-a-z0-9!#$%&'*+/=?^_`{|}~]|(?<!\.)\.)*)(?<!\.)@[a-z0-9][\w\.-]*[a-z0-9](\.[a-z][a-z\.]*[a-z])+$";

        #endregion

        #region Read-Only Fields

        private static readonly Lazy<Regex> EmailFormatRegex = new Lazy<Regex>(()
            => new Regex(EmailRegexPattern, RegexOptions.Compiled | RegexOptions.IgnoreCase));

        #endregion

        #region Properties

        public string Value { get; }

        #endregion

        #region Constructors

        private Email(string value) => Value = value;

        #endregion

        #region Factory Methods

        public static Email Create(string email)
        {
            if (email.IsNullOrWhiteSpace())
                throw new ArgumentException(DomainError.Email.NullOrEmpty.Message, nameof(email));

            if (email.Length > MaxLength)
                throw new ArgumentException(DomainError.Email.LongerThanAllowed.Message, nameof(email));

            if (!EmailFormatRegex.Value.IsMatch(email))
                throw new ArgumentException(DomainError.Email.InvalidFormat.Message, nameof(email));

            return new Email(email);
        }

        #endregion

        #region Overriden Methods

        protected override IEnumerable<object> GetAtomicValues()
        {
            yield return Value;
        }

        #endregion

        #region Operators

        public static implicit operator string(Email email)
            => email.Value;

        #endregion
    }
}
