using MediatR;
using uBee.Application.Core.Abstractions.Cryptography;
using uBee.Application.Core.Abstractions.Data;
using uBee.Domain.Core.Abstractions;
using uBee.Domain.Errors;
using uBee.Domain.Exceptions;
using uBee.Domain.Repositories;
using uBee.Domain.ValueObjects;
using uBee.Shared.Messaging;

namespace uBee.Application.Users
{
    public sealed class UpdateUserPasswordCommandHandler : ICommandHandler<UpdateUserPasswordCommand, Unit>
    {
        #region Read-Only Fields

        private readonly IUserRepository _userRepository;
        private readonly IPasswordHasher _passwordHasher;
        private readonly IPasswordHashChecker _passwordHasherChecker;
        private readonly IUnitOfWork _unitOfWork;

        #endregion

        #region Constructors

        public UpdateUserPasswordCommandHandler(
            IUserRepository userRepository,
            IPasswordHasher passwordHasher,
            IPasswordHashChecker passwordHasherChecker,
            IUnitOfWork unitOfWork)
        {
            _userRepository = userRepository;
            _passwordHasher = passwordHasher;
            _passwordHasherChecker = passwordHasherChecker;
            _unitOfWork = unitOfWork;
        }

        #endregion

        public async Task<Unit> Handle(UpdateUserPasswordCommand request, CancellationToken cancellationToken)
        {
            var user = await _userRepository.GetByIdAsync(request.UserId);
            if (user is null)
                throw new DomainException(DomainError.User.NotFound);

            if (!user.VerifyPasswordHash(request.CurrentPassword, _passwordHasherChecker))
                throw new DomainException(DomainError.Password.InvalidCurrentPassword);

            var newPasswordResult = Password.Create(request.NewPassword);
            var newPasswordHash = _passwordHasher.HashPassword(newPasswordResult);

            user.ChangePassword(newPasswordHash);

            await _unitOfWork.SaveChangesAsync(cancellationToken);

            return Unit.Value;
        }
    }
}
