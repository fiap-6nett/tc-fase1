using MediatR;
using uBee.Application.Core.Abstractions.Data;
using uBee.Domain.Errors;
using uBee.Domain.Exceptions;
using uBee.Domain.Repositories;
using uBee.Shared.Messaging;

namespace uBee.Application.Users
{
    public sealed class DeleteUserCommandHandler : ICommandHandler<DeleteUserCommand, Unit>
    {
        #region Read-Only Fields

        private readonly IUserRepository _userRepository;
        private readonly IUnitOfWork _unitOfWork;

        #endregion

        #region Constructors

        public DeleteUserCommandHandler(IUserRepository userRepository, IUnitOfWork unitOfWork)
        {
            _userRepository = userRepository;
            _unitOfWork = unitOfWork;
        }

        #endregion

        public async Task<Unit> Handle(DeleteUserCommand request, CancellationToken cancellationToken)
        {
            var user = await _userRepository.GetByIdAsync(request.UserId);
            if (user is null)
                throw new DomainException(DomainError.User.NotFound);

            if (user.IsDeleted)
                throw new DomainException(DomainError.User.AlreadyDeleted);

            await _userRepository.RemoveAsync(user);

            await _unitOfWork.SaveChangesAsync(cancellationToken);

            return Unit.Value;
        }
    }
}
