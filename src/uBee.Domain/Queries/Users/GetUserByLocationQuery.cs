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

        public int DDD { get; set; }

        #endregion

        #region Constructors

        public GetUserByLocationQuery() { }

        public GetUserByLocationQuery(int ddd)
        {
            DDD = ddd;
        }

        #endregion

        #region Methods

        public void Validate()
        {
            AddNotifications(
                new Contract<Notification>()
                    .Requires()
                    .IsBetween(DDD, 11, 99, nameof(DDD), "The DDD must be a valid number between 11 and 99.")
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
            public Guid IdLocation { get; set; }
            public EnUserRole UserRole { get; set; }

            public IReadOnlyCollection<Hive> Hives { get; set; }
            public IReadOnlyCollection<BeeContract> BeeContracts { get; set; }
        }

        #endregion
    }
}
