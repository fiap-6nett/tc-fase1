using uBee.Domain.Core.Primitives;
using Flunt.Notifications;
using Flunt.Validations;

namespace uBee.Domain.Entities
{
    public class Location : EntityBase
    {
        #region Properties
        public int DDD { get; private set; }
        public string Region { get; private set; }

        #endregion

        #region Constructors
        public Location(int ddd, string region)
        {
            AddNotifications(
                new Contract<Notification>()
                    .Requires()
                    .IsBetween(ddd, 11, 99, nameof(ddd), "The DDD must be a valid number between 11 and 99.")
                    .IsNotNullOrEmpty(region, nameof(region), "The region is required.")
            );

            if (IsValid)
            {
                DDD = ddd;
                Region = region;
            }
        }

        #endregion
    }
}
