using uBee.Application.Contracts.Common;
using uBee.Application.Contracts.Users;
using uBee.Shared.Messaging;

namespace uBee.Application.Users
{
    public record ListUsersQuery(int DDD, int Page, int PageSize) : IQuery<PagedList<UserResponse>>;
}
