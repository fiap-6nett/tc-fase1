using Flunt.Notifications;
using uBee.Domain.Commands.Users;
using uBee.Domain.Repositories;
using uBee.Shared.Commands;
using uBee.Shared.Handlers.Contracts;

namespace uBee.Application.Handlers.Users
{
    public class UpdateUserCommandHandler : Notifiable<Notification>, IHandlerCommand<UpdateUserCommand>
    {
        private readonly IUserRepository _userRepository;

        public UpdateUserCommandHandler(IUserRepository userRepository)
        {
            _userRepository = userRepository;
        }

        public async Task<ICommandResult> Handler(UpdateUserCommand command)
        {
            command.Validate();
            if (!command.IsValid)
            {
                return new GenericCommandResult(false, "Invalid command data", command.Notifications);
            }

            var user = await _userRepository.GetByIdAsync(command.Id);
            if (user == null)
            {
                return new GenericCommandResult(false, "User not found", "No user exists with the provided ID");
            }

            user.ChangeName(command.Name, command.Surname);

            if (!user.IsValid)
            {
                return new GenericCommandResult(false, "Failed to update user data", user.Notifications);
            }

            await _userRepository.UpdateNameAsync(user);

            return new GenericCommandResult(true, "User updated successfully", null);
        }
    }
}
