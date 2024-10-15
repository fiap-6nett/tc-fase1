using uBee.Domain.Entities;

namespace uBee.Domain.Repositories
{
    public interface ILocationRepository
    {
        #region ILocationRepository Members

        Task<Location> GetByIdAsync(Guid idLocation);
        Task<Location> GetByDDDAsync(int ddd);
        Task<IEnumerable<Location>> GetAllAsync();
        void Insert(Location location);

        #endregion
    }
}
