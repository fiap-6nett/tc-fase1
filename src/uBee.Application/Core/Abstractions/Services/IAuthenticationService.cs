using uBee.Application.Contracts.Authentication;

namespace uBee.Application.Core.Abstractions.Services
{
    public interface IAuthenticationService
    {
        #region IAuthenticationService Members

        Task<TokenResponse> Login(string email, string password);

        #endregion
    }
}
