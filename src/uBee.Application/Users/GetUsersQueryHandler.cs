using Microsoft.EntityFrameworkCore;
using uBee.Application.Contracts.Common;
using uBee.Application.Contracts.Users;
using uBee.Application.Core.Abstractions.Data;
using uBee.Domain.Entities;
using uBee.Domain.Enumerations;
using uBee.Domain.Errors;
using uBee.Domain.Exceptions;
using uBee.Domain.Repositories;
using uBee.Shared.Helpers;
using uBee.Shared.Messaging;

namespace uBee.Application.Users
{
    public sealed class GetUsersQueryHandler : IQueryHandler<GetUsersQuery, PagedList<UserResponse>>
    {
        #region Read-Only Fields

        private readonly IUserRepository _userRepository;
        private readonly ILocationRepository _locationRepository;
        private readonly IDbContext _dbContext;

        #endregion

        #region Constructors

        public GetUsersQueryHandler(IUserRepository userRepository, ILocationRepository locationRepository, IDbContext dbContext)
        {
            _userRepository = userRepository;
            _locationRepository = locationRepository;
            _dbContext = dbContext;
        }

        #endregion

        public async Task<PagedList<UserResponse>> Handle(GetUsersQuery request, CancellationToken cancellationToken)
        {
            IQueryable<UserResponse> usersQuery;

            if (request.DDD > 0)
            {
                var location = await _locationRepository.GetByDddAsync(request.DDD);
                if (location is null)
                    throw new DomainException(DomainError.Location.InvalidAreaCode);

                usersQuery = (
                    from user in _dbContext.Set<User, int>().AsNoTracking()
                    where user.Location.Number == request.DDD
                    orderby user.Id
                    select new UserResponse
                    {
                        Id = user.Id,
                        FullName = $"{user.Name} {user.Surname}",
                        Email = user.Email.Value,
                        DDD = user.Location.Number,
                        Location = location.Name,
                        UserRole = EnumHelper<EnUserRole>.GetDescription((EnUserRole)user.UserRole)
                    }
                );
            }
            else
            {
                usersQuery = (
                    from user in _dbContext.Set<User, int>().AsNoTracking()
                    orderby user.Id
                    select new UserResponse
                    {
                        Id = user.Id,
                        FullName = $"{user.Name} {user.Surname}",
                        Email = user.Email.Value,
                        DDD = user.Location.Number,
                        Location = user.Location.Name,
                        UserRole = EnumHelper<EnUserRole>.GetDescription((EnUserRole)user.UserRole)
                    }
                );
            }

            var totalCount = await usersQuery.CountAsync(cancellationToken);

            var usersPaged = await usersQuery
                .Skip((request.Page - 1) * request.PageSize)
                .Take(request.PageSize)
                .ToArrayAsync(cancellationToken);

            return new PagedList<UserResponse>(usersPaged, request.Page, request.PageSize, totalCount);
        }
    }
}
