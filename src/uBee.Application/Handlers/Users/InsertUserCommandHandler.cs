using Flunt.Notifications;
using uBee.Domain.Commands.Users;
using uBee.Domain.Entities;
using uBee.Domain.Repositories;
using uBee.Application.Core.Abstractions.Cryptography;
using uBee.Shared.Commands;
using uBee.Shared.Handlers.Contracts;

namespace uBee.Application.Handlers.Users
{
    public class InsertUserCommandHandler : Notifiable<Notification>, IHandlerCommand<InsertUserCommand>
    {
        private readonly IUserRepository _userRepository;
        private readonly IPasswordHasher _passwordHasher;

        public InsertUserCommandHandler(IUserRepository userRepository, IPasswordHasher passwordHasher)
        {
            _userRepository = userRepository;
            _passwordHasher = passwordHasher;
        }

        public async Task<ICommandResult> Handler(InsertUserCommand command)
        {
            command.Validate();
            if (!command.IsValid)
            {
                return new GenericCommandResult(false, "Please correct the provided user data", command.Notifications);
            }

            var emailExists = await _userRepository.CheckEmailInUseAsync(command.Email);
            if (emailExists)
            {
                return new GenericCommandResult(false, "Email is already in use", "Please use another email");
            }

            command.Password = _passwordHasher.Encrypt(command.Password);

            var user = new User(command.Name, command.Surname, command.Email, command.Phone, command.Password, command.UserRole, command.Location);

            if (!user.IsValid)
            {
                return new GenericCommandResult(false, "Invalid user data", user.Notifications);
            }

            await _userRepository.InsertAsync(user);

            return new GenericCommandResult(true, "User created successfully!", "user-token");
        }
    }
}
