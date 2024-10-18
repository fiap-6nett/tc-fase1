using Flunt.Notifications;
using Flunt.Validations;
using uBee.Shared.Commands;

namespace uBee.Domain.Commands.Authentication
{
    public class LoginCommand : Notifiable<Notification>, ICommand
    {
        #region Properties

        public string Email { get; set; }
        public string Password { get; set; }

        #endregion

        #region Constructors

        public LoginCommand(string email, string password)
        {
            Email = email;
            Password = password;
        }

        #endregion

        #region Methods

        public void Validate()
        {
            AddNotifications(
            new Contract<Notification>()
                .Requires()
            );
        }

        #endregion
    }
}
