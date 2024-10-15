namespace uBee.Infrastructure.Cryptography
{
    public static class Password
    {
        public static string Encrypt(string password) => BCrypt.Net.BCrypt.HashPassword(password);

        public static bool ValidateHashes(string password, string hash) => BCrypt.Net.BCrypt.Verify(password, hash);
    }
}
