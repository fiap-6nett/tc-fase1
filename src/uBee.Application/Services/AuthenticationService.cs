using uBee.Application.Contracts.Authentication;
using uBee.Application.Core.Abstractions.Authentication;
using uBee.Application.Core.Abstractions.Services;
using uBee.Domain.Core.Abstractions;
using uBee.Domain.Errors;
using uBee.Domain.Exceptions;
using uBee.Domain.Repositories;

namespace uBee.Application.Services
{
    internal sealed class AuthenticationService : IAuthenticationService
    {
        #region Read-Only Fields

        private readonly IJwtProvider _jwtProvider;
        private readonly IUserRepository _userRepository;
        private readonly IPasswordHashChecker _passwordHashChecker;

        #endregion

        #region Constructors

        public AuthenticationService(IJwtProvider jwtProvider, IUserRepository userRepository, IPasswordHashChecker passwordHashChecker)
        {
            _jwtProvider = jwtProvider ?? throw new ArgumentNullException(nameof(jwtProvider));
            _userRepository = userRepository ?? throw new ArgumentNullException(nameof(userRepository));
            _passwordHashChecker = passwordHashChecker ?? throw new ArgumentNullException(nameof(passwordHashChecker));
        }

        #endregion

        #region IAuthenticationService Members

        public async Task<TokenResponse> Login(string email, string password)
        {
            var emailResult = email;

            var user = await _userRepository.GetByEmailAsync(emailResult);
            if (user is null)
                throw new DomainException(DomainError.Authentication.InvalidEmailOrPassword);

            var passwordValid = user.VerifyPasswordHash(password, _passwordHashChecker);
            if (!passwordValid)
                throw new DomainException(DomainError.Authentication.InvalidEmailOrPassword);

            return new TokenResponse(_jwtProvider.Create(user));
        }

        #endregion
    }
}
