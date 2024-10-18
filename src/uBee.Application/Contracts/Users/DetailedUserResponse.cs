using uBee.Domain.Enumerations;

namespace uBee.Application.Contracts.Users
{
    public sealed class DetailedUserResponse
    {
        /// <summary>
        /// Gets or sets the user identifier.
        /// </summary>
        public Guid IdUser { get; set; }

        /// <summary>
        /// Gets or sets the user name.
        /// </summary>
        public string Name { get; set; }

        /// <summary>
        /// Gets or sets the user surname.
        /// </summary>
        public string Surname { get; set; }

        /// <summary>
        /// Gets or sets the user email.
        /// </summary>
        public string Email { get; set; }

        /// <summary>
        /// Gets or sets the user phone.
        /// </summary>
        public string Phone { get; set; }

        /// <summary>
        /// Gets or sets the user location.
        /// </summary>
        public EnLocation Location { get; set; }

        /// <summary>
        /// Gets or sets the user role.
        /// </summary>
        public EnUserRole UserRole { get; set; }

        /// <summary>
        /// Gets the user's creation date.
        /// </summary>
        public DateTime CreatedAt { get; set; }

        /// <summary>
        /// Gets the user's last updated date.
        /// </summary>
        public DateTime? LastUpdatedAt { get; set; }
    }
}
