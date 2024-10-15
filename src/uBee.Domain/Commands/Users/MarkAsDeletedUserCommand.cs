using Flunt.Notifications;
using Flunt.Validations;
using MediatR;
using uBee.Shared.Commands;

namespace uBee.Domain.Commands.Users
{
    public class MarkAsDeletedUserCommand : Notifiable<Notification>, ICommand, IRequest<GenericCommandResult>
    {
        #region Properties

        public Guid IdUser { get; private set; }

        #endregion

        #region Constructors

        public MarkAsDeletedUserCommand() { }

        public MarkAsDeletedUserCommand(Guid idUser)
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
    }
}
