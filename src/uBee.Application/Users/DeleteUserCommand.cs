using MediatR;
using uBee.Application.Core.Messagings;

namespace uBee.Application.Users
{
    public record DeleteUserCommand(int UserId) : ICommand<Unit>;
}
