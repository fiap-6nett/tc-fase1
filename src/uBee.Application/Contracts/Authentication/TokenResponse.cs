namespace uBee.Application.Contracts.Authentication
{
    /// <summary>
    /// Represents the response containing the authentication token.
    /// </summary>
    public sealed class TokenResponse
    {
        /// <summary>
        /// Gets the token generated for the authenticated user.
        /// </summary>
        public string Token { get; }

        /// <summary>
        /// Initializes a new instance of the <see cref="TokenResponse"/> class.
        /// </summary>
        /// <param name="token">The generated token.</param>
        public TokenResponse(string token) => Token = token;
    }
}
