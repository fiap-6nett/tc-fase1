using uBee.Domain.Enumerations;

namespace uBee.Application.Contracts.Users
{
    /// <summary>
    /// Represents the user response.
    /// </summary>
    public sealed class UserResponse
    {
        /// <summary>
        /// Gets or sets the user identifier.
        /// </summary>
        public Guid IdUser { get; set; }

        /// <summary>
        /// Gets or sets the user fullname.
        /// </summary>
        public string FullName { get; set; }

        /// <summary>
        /// Gets or sets the user fullphone.
        /// </summary>
        public string FullPhone { get; set; }

        /// <summary>
        /// Gets or sets the user role.
        /// </summary>
        public EnUserRole UserRole { get; set; }
    }
}
