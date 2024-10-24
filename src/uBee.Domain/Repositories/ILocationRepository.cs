using uBee.Domain.Entities;

namespace uBee.Domain.Repositories
{
    public interface ILocationRepository
    {
        #region ILocationRepository Members

        /// <summary>
        /// Retrieves a location by its ID.
        /// </summary>
        /// <param name="locationId">The location ID.</param>
        /// <returns>The location entity.</returns>
        Task<Location> GetByIdAsync(int locationId);

        /// <summary>
        /// Retrieves a location by its DDD (area code).
        /// </summary>
        /// <param name="ddd">The DDD (area code).</param>
        /// <returns>The location entity that matches the given DDD.</returns>
        Task<Location> GetByDddAsync(int ddd);

        /// <summary>
        /// Checks if a DDD is unique within the system.
        /// </summary>
        /// <param name="ddd">The DDD to check.</param>
        /// <returns>True if the DDD is unique, false otherwise.</returns>
        Task<bool> IsDddUniqueAsync(int ddd);

        /// <summary>
        /// Retrieves all locations.
        /// </summary>
        /// <returns>A list of all location entities.</returns>
        Task<IEnumerable<Location>> GetAllAsync();

        /// <summary>
        /// Inserts a new location into the repository.
        /// </summary>
        /// <param name="location">The location entity to insert.</param>
        Task InsertAsync(Location location);

        #endregion
    }
}
