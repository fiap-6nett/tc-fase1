using uBee.Application.Contracts.Users;
using uBee.Shared.Messaging;

namespace uBee.Application.Users
{
    public record GetUserByIdQuery(int IdUser) : IQuery<DetailedUserResponse>;
}
