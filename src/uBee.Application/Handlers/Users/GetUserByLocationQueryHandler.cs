using Flunt.Notifications;
using uBee.Domain.Queries.Users;
using uBee.Domain.Repositories;
using uBee.Shared.Handlers.Contracts;
using uBee.Shared.Queries;

namespace uBee.Application.Handlers.Users
{
    public class GetUserByLocationQueryHandler : Notifiable<Notification>, IHandlerQuery<GetUserByLocationQuery>
    {
        private readonly IUserRepository _userRepository;

        public GetUserByLocationQueryHandler(IUserRepository userRepository)
        {
            _userRepository = userRepository;
        }

        public async Task<IQueryResult> Handler(GetUserByLocationQuery query)
        {
            // Validar o query
            query.Validate();
            if (!query.IsValid)
            {
                return new GenericQueryResult(false, "Invalid query data", query.Notifications);
            }

            var users = await _userRepository.GetByLocationAsync(query.DDD);
            if (users == null || !users.Any())
            {
                return new GenericQueryResult(false, "No users found for the specified DDD", null);
            }

            var result = users.Select(user => new GetUserByLocationQuery.GetUserByLocationResult
            {
                Id = user.Id,
                Name = user.Name,
                Surname = user.Surname,
                Email = user.Email,
                Phone = user.Phone,
                IdLocation = user.IdLocation,
                UserRole = user.UserRole,
                Hives = user.Hives,
                BeeContracts = user.BeeContracts
            }).ToList();

            return new GenericQueryResult(true, "Users retrieved successfully", result);
        }
    }
}
