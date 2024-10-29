using System.Text.RegularExpressions;
using uBee.Domain.Core.Primitives;
using uBee.Domain.Errors;
using uBee.Shared.Extensions;

public sealed class Phone : ValueObject
{
    #region Constants

    public const int MaxLength = 12;
    private const string PhoneRegexPattern = @"^[1-9]{2}-[0-9]{8,9}$";

    #endregion

    #region Properties

    public string Value { get; private set; }

    public int DDD => int.Parse(Value.Split('-')[0]);

    public string PhoneNumberWithoutDdd => Value.Split('-')[1];

    #endregion

    #region Constructors

    private Phone() { }

    private Phone(string value)
    {
        Value = value;
    }

    #endregion

    #region Factory Methods

    public static Phone Create(string phone)
    {
        if (phone.IsNullOrWhiteSpace())
            throw new ArgumentException(DomainError.Phone.InvalidFormat.Message, nameof(phone));

        var phoneRegex = new Regex(PhoneRegexPattern);
        if (!phoneRegex.IsMatch(phone))
            throw new ArgumentException(DomainError.Phone.InvalidFormat.Message, nameof(phone));

        if (phone.Length != MaxLength && phone.Length != MaxLength - 1)
            throw new ArgumentException(DomainError.Phone.InvalidFormat.Message, nameof(phone));

        return new Phone(phone);
    }

    #endregion

    #region Overridden Methods

    protected override IEnumerable<object> GetAtomicValues()
    {
        yield return Value;
    }

    #endregion

    #region Operators

    public static implicit operator string(Phone phone)
        => phone?.Value ?? string.Empty;

    #endregion
}
