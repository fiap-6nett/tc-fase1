using MediatR;
using uBee.Application.Core.Messagings;

namespace uBee.Application.Users
{
    public record UpdateUserPasswordCommand(int UserId, string CurrentPassword, string NewPassword) : ICommand<Unit>;
}
