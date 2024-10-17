using uBee.Application.Core.Abstractions.Cryptography;

namespace uBee.Infrastructure.Cryptography
{
    public class PasswordHasher : IPasswordHasher
    {
        public string Encrypt(string password) => BCrypt.Net.BCrypt.HashPassword(password);

        public bool ValidateHashes(string password, string hash) => BCrypt.Net.BCrypt.Verify(password, hash);
    }
}
