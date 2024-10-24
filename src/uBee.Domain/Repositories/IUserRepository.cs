using uBee.Domain.Entities;

namespace uBee.Domain.Repositories
{
    public interface IUserRepository
    {
        #region IUserRepository Members

        /// <summary>
        /// Retrieves a user by their ID.
        /// </summary>
        /// <param name="idUser">The user ID.</param>
        /// <returns>The user entity.</returns>
        Task<User> GetByIdAsync(int idUser);

        /// <summary>
        /// Retrieves a user by their email.
        /// </summary>
        /// <param name="email">The user's email.</param>
        /// <returns>The user entity.</returns>
        Task<User> GetByEmailAsync(string email);

        /// <summary>
        /// Checks if an email is unique within the system.
        /// </summary>
        /// <param name="email">The email to check.</param>
        /// <returns>True if the email is unique, false otherwise.</returns>
        Task<bool> IsEmailUniqueAsync(string email);

        /// <summary>
        /// Retrieves users by their location, filtered by DDD number or location name.
        /// </summary>
        /// <param name="dddNumber">The DDD number to filter by.</param>
        /// <param name="locationName">The location name to filter by.</param>
        /// <returns>A list of users in the specified location (by DDD or name).</returns>
        Task<IEnumerable<User>> GetByLocationAsync(int? dddNumber = null, string locationName = null);

        /// <summary>
        /// Inserts a new user into the repository.
        /// </summary>
        /// <param name="user">The user entity to insert.</param>
        Task InsertAsync(User user);

        #endregion
    }
}
