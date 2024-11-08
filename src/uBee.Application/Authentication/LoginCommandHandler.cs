using uBee.Application.Contracts.Authentication;
using uBee.Application.Core.Abstractions.Authentication;
using uBee.Application.Core.Messagings;
using uBee.Application.Repositories;
using uBee.Domain.Core.Abstractions;
using uBee.Domain.Errors;
using uBee.Domain.Exceptions;

namespace uBee.Application.Authentication
{
    public sealed class LoginCommandHandler : ICommandHandler<LoginCommand, TokenResponse>
    {
        #region Read-Only Fields

        private readonly IJwtProvider _jwtProvider;
        private readonly IUserRepository _userRepository;
        private readonly IPasswordHashChecker _passwordHashChecker;

        #endregion

        #region Constructors

        public LoginCommandHandler(IJwtProvider jwtProvider, IUserRepository userRepository, IPasswordHashChecker passwordHashChecker)
        {
            _jwtProvider = jwtProvider ?? throw new ArgumentNullException(nameof(jwtProvider));
            _userRepository = userRepository ?? throw new ArgumentNullException(nameof(userRepository));
            _passwordHashChecker = passwordHashChecker ?? throw new ArgumentNullException(nameof(passwordHashChecker));
        }

        #endregion

        public async Task<TokenResponse> Handle(LoginCommand request, CancellationToken cancellationToken)
        {
            var user = await _userRepository.GetByEmailAsync(request.Email);

            if (user is null)
                throw new DomainException(DomainError.Authentication.EmailNotRegistered);

            var passwordValid = user.VerifyPasswordHash(request.Password, _passwordHashChecker);
            if (!passwordValid)
                throw new DomainException(DomainError.Authentication.InvalidEmailOrPassword);

            return new TokenResponse(_jwtProvider.Create(user));
        }
    }
}
