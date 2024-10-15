using Flunt.Notifications;
using Flunt.Validations;
using MediatR;
using uBee.Domain.Enumerations;
using uBee.Shared.Commands;

namespace uBee.Domain.Commands.Users
{
    public class InsertUserCommand : Notifiable<Notification>, ICommand, IRequest<GenericCommandResult>
    {
        #region Properties

        public string Name { get; set; }
        public string Surname { get; set; }
        public string Email { get; set; }
        public string Phone { get; set; }
        public string Password { get; set; }
        public EnUserRole UserRole { get; set; }
        public Guid IdLocation { get; set; }

        #endregion

        #region Constructors

        public InsertUserCommand() { }

        public InsertUserCommand(string name, string surname, string email, string phone, string password, EnUserRole userRole, Guid idLocation)
        {
            Name = name;
            Surname = surname;
            Email = email;
            Phone = phone;
            Password = password;
            UserRole = userRole;
            IdLocation = idLocation;
        }

        #endregion

        #region Methods

        public void Validate()
        {
            AddNotifications(
                new Contract<Notification>()
                    .Requires()
                    .IsNotEmpty(Name, nameof(Name), "The name is required.")
                    .IsNotEmpty(Surname, nameof(Surname), "The surname is required.")
                    .IsEmail(Email, nameof(Email), "A valid email is required.")
                    .IsNotEmpty(Phone, nameof(Phone), "The phone number is required.")
                    .IsNotEmpty(Password, nameof(Password), "The password is required.")
                    .IsTrue(Enum.IsDefined(typeof(EnUserRole), UserRole), nameof(UserRole), "The user role is invalid.")
                    .IsFalse(IdLocation == Guid.Empty, nameof(IdLocation), "The location ID is required.")
            );
        }

        #endregion
    }
}
