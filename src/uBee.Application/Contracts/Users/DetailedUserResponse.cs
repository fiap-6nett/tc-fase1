namespace uBee.Application.Contracts.Users
{
    /// <summary>
    /// Represents a detailed user response in the system.
    /// </summary>
    public class DetailedUserResponse
    {
        /// <summary>
        /// Gets or sets the unique identifier of the user.
        /// </summary>
        public int Id { get; set; }

        /// <summary>
        /// Gets or sets the full name of the user.
        /// </summary>
        public string FullName { get; set; }

        /// <summary>
        /// Gets or sets the CPF of the user.
        /// </summary>
        public string Cpf { get; set; }

        /// <summary>
        /// Gets or sets the email of the user.
        /// </summary>
        public string Email { get; set; }

        /// <summary>
        /// Gets or sets the phone number of the user.
        /// </summary>
        public string Phone { get; set; }

        /// <summary>
        /// Gets or sets the DDD (area code) of the user.
        /// </summary>
        public int DDD { get; set; }

        /// <summary>
        /// Gets or sets the description of the user's location (e.g., São Paulo).
        /// </summary>
        public string Location { get; set; }

        /// <summary>
        /// Gets or sets the role of the user as a friendly string (e.g., Beekeeper).
        /// </summary>
        public string UserRole { get; set; }
    }
}
