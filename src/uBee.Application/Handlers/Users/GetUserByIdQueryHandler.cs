using Flunt.Notifications;
using uBee.Domain.Queries.Users;
using uBee.Domain.Repositories;
using uBee.Shared.Handlers.Contracts;
using uBee.Shared.Queries;

namespace uBee.Application.Handlers.Users
{
    public class GetUserByIdQueryHandler : Notifiable<Notification>, IHandlerQuery<GetUserByIdQuery>
    {
        private readonly IUserRepository _userRepository;

        public GetUserByIdQueryHandler(IUserRepository userRepository)
        {
            _userRepository = userRepository;
        }

        public async Task<IQueryResult> Handler(GetUserByIdQuery query)
        {
            query.Validate();
            if (!query.IsValid)
            {
                return new GenericQueryResult(false, "Invalid query data", query.Notifications);
            }

            var user = await _userRepository.GetByIdAsync(query.IdUser);
            if (user == null)
            {
                return new GenericQueryResult(false, "User not found", null);
            }

            var result = new GetUserByIdQuery.GetUserByIdResult(
                user.Id,
                user.Name,
                user.Surname,
                user.Email,
                user.Phone,
                user.Location,
                user.UserRole,
                user.Hives,
                user.BeeContracts
            );

            return new GenericQueryResult(true, "User retrieved successfully", result);
        }
    }
}
