using System.Linq;
using uBee.Domain.Core.Primitives;
using uBee.Domain.Errors;

namespace uBee.Domain.ValueObjects
{
    public sealed class CPF : ValueObject
    {
        #region Constants

        public const int MaxLength = 11;

        #endregion

        #region Properties

        public string Value { get; }

        #endregion

        #region Constructors

        private CPF(string value) => Value = value;

        #endregion

        #region Factory Methods

        public static CPF Create(string cpf)
        {
            if (string.IsNullOrWhiteSpace(cpf))
                throw new ArgumentException(DomainError.Cpf.InvalidFormat.Message, nameof(cpf));

            var cleanCPF = CleanCpf(cpf);
            if (cleanCPF.Length != MaxLength)
                throw new ArgumentException(DomainError.Cpf.InvalidFormat.Message, nameof(cpf));

            if (!IsValidCPF(cleanCPF))
                throw new ArgumentException(DomainError.Cpf.InvalidChecksum.Message, nameof(cpf));

            return new CPF(cleanCPF);
        }

        private static string CleanCpf(string cpf)
        {
            return cpf.Trim().Replace(".", "").Replace("-", "");
        }

        private static bool IsValidCPF(string cpf)
        {
            if (cpf.All(c => c == cpf[0])) return false;

            var digits = cpf.Select(c => int.Parse(c.ToString())).ToArray();
            var firstCheckSum = GetCPFChecksum(digits, 9);
            var secondCheckSum = GetCPFChecksum(digits, 10);

            return digits[9] == firstCheckSum && digits[10] == secondCheckSum;
        }

        private static int GetCPFChecksum(int[] digits, int length)
        {
            var sum = 0;
            for (var i = 0; i < length; i++)
            {
                sum += digits[i] * (length + 1 - i);
            }

            var remainder = sum % 11;
            return remainder < 2 ? 0 : 11 - remainder;
        }

        #endregion

        #region Overriden Methods

        protected override IEnumerable<object> GetAtomicValues()
        {
            yield return Value;
        }

        #endregion

        #region Operators

        public static implicit operator string(CPF cpf)
            => cpf.Value;

        #endregion
    }
}
