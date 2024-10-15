using Flunt.Notifications;

namespace uBee.Domain.Core.Primitives
{
    public abstract class EntityBase : Notifiable<Notification>
    {
        #region Properties
        public Guid Id { get; private set; }

        #endregion

        #region Constructors
        protected EntityBase()
        {
            Id = Guid.NewGuid();
        }

        #endregion
    }
}
