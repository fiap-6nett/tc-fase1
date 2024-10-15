using Flunt.Notifications;
using Flunt.Validations;
using MediatR;
using uBee.Domain.Entities;
using uBee.Domain.Enumerations;
using uBee.Shared.Queries;

namespace uBee.Domain.Queries.Users
{
    public class GetUserByIdQuery : Notifiable<Notification>, IQuery, IRequest<GenericQueryResult>
    {
        #region Properties

        public Guid IdUser { get; set; }

        #endregion

        #region Constructors

        public GetUserByIdQuery() { }

        public GetUserByIdQuery(Guid idUser)
        {
            IdUser = idUser;
        }

        #endregion

        #region Methods

        public void Validate()
        {
            AddNotifications(
                new Contract<Notification>()
                    .Requires()
                    .IsFalse(IdUser == Guid.Empty, nameof(IdUser), "The user ID is required.")
            );
        }

        #endregion

        #region Result Class

        public class GetUserByIdResult
        {
            public Guid Id { get; set; }
            public string Name { get; set; }
            public string Surname { get; set; }
            public string Email { get; set; }
            public string Phone { get; set; }
            public Guid IdLocation { get; set; }
            public EnUserRole UserRole { get; set; }

            public IReadOnlyCollection<Hive> Hives { get; }
            public IReadOnlyCollection<BeeContract> BeeContracts { get; }

            public GetUserByIdResult(Guid id, string name, string surname, string email, string phone, Guid idLocation, EnUserRole userRole, IReadOnlyCollection<Hive> hives, IReadOnlyCollection<BeeContract> beeContracts)
            {
                Id = id;
                Name = name;
                Surname = surname;
                Email = email;
                Phone = phone;
                IdLocation = idLocation;
                UserRole = userRole;
                Hives = hives;
                BeeContracts = beeContracts;
            }
        }

        #endregion
    }
}
