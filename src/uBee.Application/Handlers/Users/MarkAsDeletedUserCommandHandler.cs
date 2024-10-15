using Flunt.Notifications;
using uBee.Domain.Commands.Users;
using uBee.Domain.Repositories;
using uBee.Shared.Commands;
using uBee.Shared.Handlers.Contracts;

namespace uBee.Application.Handlers.Users
{
    public class MarkAsDeletedUserCommandHandler : Notifiable<Notification>, IHandlerCommand<MarkAsDeletedUserCommand>
    {
        private readonly IUserRepository _userRepository;

        public MarkAsDeletedUserCommandHandler(IUserRepository userRepository)
        {
            _userRepository = userRepository;
        }

        public async Task<ICommandResult> Handler(MarkAsDeletedUserCommand command)
        {
            command.Validate();
            if (!command.IsValid)
            {
                return new GenericCommandResult(false, "Invalid user ID", command.Notifications);
            }

            var user = await _userRepository.GetByIdAsync(command.IdUser);
            if (user == null)
            {
                return new GenericCommandResult(false, "User not found", null);
            }

            user.MarkAsDeleted();

            await _userRepository.MarkAsDeleted(user);

            return new GenericCommandResult(true, "User marked as deleted successfully", null);
        }
    }
}
