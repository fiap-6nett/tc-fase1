using uBee.Application.Contracts.Users;
using uBee.Domain.Enumerations;
using uBee.Domain.Errors;
using uBee.Domain.Exceptions;
using uBee.Domain.Repositories;
using uBee.Shared.Helpers;
using uBee.Shared.Messaging;

namespace uBee.Application.Users
{
    public sealed class GetUserByIdQueryHandler : IQueryHandler<GetUserByIdQuery, DetailedUserResponse>
    {
        #region Read-Only Fields

        private readonly IUserRepository _userRepository;
        private readonly ILocationRepository _locationRepository;

        #endregion

        #region Constructors

        public GetUserByIdQueryHandler(IUserRepository userRepository, ILocationRepository locationRepository)
        {
            _userRepository = userRepository ?? throw new ArgumentNullException(nameof(userRepository));
            _locationRepository = locationRepository;
        }

        #endregion

        public async Task<DetailedUserResponse> Handle(GetUserByIdQuery request, CancellationToken cancellationToken)
        {
            var user = await _userRepository.GetByIdAsync(request.IdUser);

            if (user is null)
                throw new DomainException(DomainError.User.NotFound);

            var location = await _locationRepository.GetByIdAsync(user.LocationId);

            return new DetailedUserResponse
            {
                Id = user.Id,
                FullName = $"{user.Name} {user.Surname}",
                Cpf = user.CPF.Value,
                Email = user.Email.Value,
                Phone = user.Phone.Value,
                DDD = location.Number,
                Location = location.Name,
                UserRole = EnumHelper<EnUserRole>.GetDescription((EnUserRole)user.UserRole)
            };
        }
    }
}
