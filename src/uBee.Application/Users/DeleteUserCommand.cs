using MediatR;
using uBee.Shared.Messaging;

namespace uBee.Application.Users
{
    public record DeleteUserCommand(int UserId) : ICommand<Unit>;
}
