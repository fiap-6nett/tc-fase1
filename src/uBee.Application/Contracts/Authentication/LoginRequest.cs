namespace uBee.Application.Contracts.Authentication
{
    /// <summary>
    /// Represents a request to log in to the system.
    /// </summary>
    public sealed class LoginRequest
    {
        /// <summary>
        /// Gets or sets the email of the user trying to log in.
        /// </summary>
        public string Email { get; set; }

        /// <summary>
        /// Gets or sets the password of the user trying to log in.
        /// </summary>
        public string Password { get; set; }
    }
}
