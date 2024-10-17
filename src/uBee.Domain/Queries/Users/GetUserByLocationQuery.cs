using Flunt.Notifications;
using Flunt.Validations;
using MediatR;
using uBee.Domain.Entities;
using uBee.Domain.Enumerations;
using uBee.Shared.Queries;

namespace uBee.Domain.Queries.Users
{
    public class GetUserByLocationQuery : Notifiable<Notification>, IQuery, IRequest<GenericQueryResult>
    {
        #region Properties

        public EnLocation Location { get; set; }

        #endregion

        #region Constructors

        public GetUserByLocationQuery() { }

        public GetUserByLocationQuery(EnLocation location)
        {
            Location = location;
        }

        #endregion

        #region Methods

        public void Validate()
        {
            AddNotifications(
                new Contract<Notification>()
                    .Requires()
                    .IsTrue(Enum.IsDefined(typeof(EnLocation), Location), nameof(Location), "The location must be a valid value from EnLocation.")
            );
        }

        #endregion

        #region Result Class

        public class GetUserByLocationResult
        {
            public Guid Id { get; set; }
            public string Name { get; set; }
            public string Surname { get; set; }
            public string Email { get; set; }
            public string Phone { get; set; }
            public EnLocation Location { get; set; }
            public EnUserRole UserRole { get; set; }

            public IReadOnlyCollection<Hive> Hives { get; set; }
            public IReadOnlyCollection<BeeContract> BeeContracts { get; set; }
        }

        #endregion
    }
}
