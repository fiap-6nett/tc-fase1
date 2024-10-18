using uBee.Domain.ValueObjects;

namespace uBee.Application.Core.Abstractions.Cryptography
{
    public interface IPasswordHasher
    {
        string HashPassword(Password password);
    }
}
