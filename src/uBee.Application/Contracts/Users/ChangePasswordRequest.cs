namespace uBee.Application.Contracts.Users
{
    /// <summary>
    /// Represents a request to change the password of a user.
    /// </summary>
    public sealed class ChangePasswordRequest
    {
        /// <summary>
        /// Gets or sets the current password of the user.
        /// </summary>
        public string CurrentPassword { get; set; }

        /// <summary>
        /// Gets or sets the new password that the user wants to set.
        /// </summary>
        public string NewPassword { get; set; }
    }
}
