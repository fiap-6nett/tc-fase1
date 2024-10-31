using uBee.Domain.Core.Primitives;
using uBee.Domain.Core.Utility.Extensions;
using uBee.Domain.Errors;

namespace uBee.Domain.ValueObjects
{
    public sealed class Password : ValueObject
    {
        #region Constants

        private const int MinPasswordLength = 6;

        #endregion

        #region Read-Only Fields

        private static readonly Func<char, bool> IsLower = c => char.IsLower(c);
        private static readonly Func<char, bool> IsUpper = c => char.IsUpper(c);
        private static readonly Func<char, bool> IsDigit = c => char.IsDigit(c);
        private static readonly Func<char, bool> IsNonAlphaNumeric = c => !char.IsLetterOrDigit(c);

        #endregion

        #region Properties

        public string Value { get; }

        #endregion

        #region Constructors

        private Password(string value) => Value = value;

        #endregion

        #region Factory Methods

        public static Password Create(string password)
        {
            if (password.IsNullOrWhiteSpace())
                throw new ArgumentException(DomainError.Password.NullOrEmpty.Message, nameof(password));

            if (password.Length < MinPasswordLength)
                throw new ArgumentException(DomainError.Password.TooShort.Message, nameof(password));

            if (!password.Any(IsLower))
                throw new ArgumentException(DomainError.Password.MissingLowercaseLetter.Message, nameof(password));

            if (!password.Any(IsUpper))
                throw new ArgumentException(DomainError.Password.MissingUppercaseLetter.Message, nameof(password));

            if (!password.Any(IsDigit))
                throw new ArgumentException(DomainError.Password.MissingDigit.Message, nameof(password));

            if (!password.Any(IsNonAlphaNumeric))
                throw new ArgumentException(DomainError.Password.MissingNonAlphaNumeric.Message, nameof(password));

            return new Password(password);
        }

        #endregion

        #region Overridden Methods

        protected override IEnumerable<object> GetAtomicValues()
        {
            yield return Value;
        }

        #endregion

        #region Operators

        public static implicit operator string(Password password)
            => password?.Value ?? string.Empty;

        #endregion
    }
}
