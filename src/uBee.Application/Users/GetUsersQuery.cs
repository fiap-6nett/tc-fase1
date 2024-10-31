using uBee.Application.Contracts.Common;
using uBee.Application.Contracts.Users;
using uBee.Application.Core.Messagings;

namespace uBee.Application.Users
{
    public record GetUsersQuery(int DDD, int Page, int PageSize) : IQuery<PagedList<UserResponse>>;
}
