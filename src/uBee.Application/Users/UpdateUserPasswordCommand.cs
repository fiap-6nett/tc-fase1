using MediatR;
using uBee.Shared.Messaging;

namespace uBee.Application.Users
{
    public record UpdateUserPasswordCommand(int UserId, string CurrentPassword, string NewPassword) : ICommand<Unit>;
}
