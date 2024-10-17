namespace uBee.Application.Core.Abstractions.Cryptography
{
    public interface IPasswordHasher
    {
        string Encrypt(string password);
        bool ValidateHashes(string password, string hash);
    }
}
