using uBee.Application.Contracts.Authentication;
using uBee.Application.Core.Abstractions.Authentication;
using uBee.Application.Core.Abstractions.Cryptography;
using uBee.Application.Core.Abstractions.Data;
using uBee.Domain.Entities;
using uBee.Domain.Errors;
using uBee.Domain.Exceptions;
using uBee.Domain.Repositories;
using uBee.Domain.ValueObjects;
using uBee.Shared.Messaging;

namespace uBee.Application.Authentication
{
    public class RegisterCommandHandler : ICommandHandler<RegisterCommand, TokenResponse>
    {
        #region Read-Only Fields

        private readonly IJwtProvider _jwtProvider;
        private readonly IUserRepository _userRepository;
        private readonly ILocationRepository _locationRepository;
        private readonly IPasswordHasher _passwordHasher;
        private readonly IUnitOfWork _unitOfWork;

        #endregion

        #region Constructors

        public RegisterCommandHandler(
            IJwtProvider jwtProvider,
            IUserRepository userRepository,
            ILocationRepository locationRepository,
            IPasswordHasher passwordHasher,
            IUnitOfWork unitOfWork)
        {
            _jwtProvider = jwtProvider ?? throw new ArgumentNullException(nameof(jwtProvider));
            _userRepository = userRepository ?? throw new ArgumentNullException(nameof(userRepository));
            _locationRepository = locationRepository ?? throw new ArgumentNullException(nameof(locationRepository));
            _passwordHasher = passwordHasher ?? throw new ArgumentNullException(nameof(passwordHasher));
            _unitOfWork = unitOfWork ?? throw new ArgumentNullException(nameof(unitOfWork));
        }

        #endregion

        public async Task<TokenResponse> Handle(RegisterCommand request, CancellationToken cancellationToken)
        {
            var emailResult = Email.Create(request.Email);
            if (!await _userRepository.IsEmailUniqueAsync(emailResult))
                throw new DomainException(DomainError.User.DuplicateEmail);

            var cpfResult = CPF.Create(request.Cpf);
            if (!await _userRepository.IsCpfUniqueAsync(cpfResult.Value))
                throw new DomainException(DomainError.User.DuplicateCpf);

            var phoneResult = Phone.Create(request.Phone);
            if (!await _userRepository.IsPhoneUniqueAsync(phoneResult.Value))
                throw new DomainException(DomainError.User.DuplicatePhone);

            var ddd = int.Parse(phoneResult.GetDdd());
            var location = await _locationRepository.GetByDddAsync(ddd);
            if (location is null)
                throw new DomainException(DomainError.Location.InvalidAreaCode);

            var passwordHash = _passwordHasher.HashPassword(Password.Create(request.Password));
            var user = new User(
                request.Name,
                request.Surname,
                cpfResult,
                emailResult,
                phoneResult,
                passwordHash,
                (byte)request.UserRole,
                location.Id
            );

            await _userRepository.InsertAsync(user);
            await _unitOfWork.SaveChangesAsync(cancellationToken);

            return new TokenResponse(_jwtProvider.Create(user));
        }
    }
}
