using uBee.Application.Contracts.Users;
using uBee.Application.Core.Messagings;

namespace uBee.Application.Users
{
    public record GetUserByIdQuery(int IdUser) : IQuery<DetailedUserResponse>;
}
