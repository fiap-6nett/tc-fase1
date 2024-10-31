using Microsoft.EntityFrameworkCore;
using uBee.Application.Repositories;
using uBee.Domain.Entities;
using uBee.Persistence.Core.Primitives;

namespace uBee.Persistence.Repositories
{
    internal sealed class LocationRepository : GenericRepository<Location, byte>, ILocationRepository
    {
        #region Private Fields

        private readonly uBeeContext _context;

        #endregion

        #region Constructors

        public LocationRepository(uBeeContext context) : base(context)
        {
            _context = context;
        }

        #endregion

        #region ILocationRepository Members

        /// <summary>
        /// Retrieves a location by its ID.
        /// </summary>
        public async Task<Location> GetByIdAsync(int locationId)
            => await FirstOrDefaultAsync(location => location.Id == locationId);

        /// <summary>
        /// Retrieves a location by its DDD (area code).
        /// </summary>
        public async Task<Location> GetByDddAsync(int ddd)
            => await FirstOrDefaultAsync(location => location.Number == ddd);

        /// <summary>
        /// Checks if a DDD is unique within the system.
        /// </summary>
        public async Task<bool> IsDddUniqueAsync(int ddd)
            => !await AnyAsync(location => location.Number == ddd);

        /// <summary>
        /// Retrieves all locations.
        /// </summary>
        public async Task<IEnumerable<Location>> GetAllAsync()
            => await _context.Locations.ToListAsync();

        /// <summary>
        /// Inserts a new location into the repository.
        /// </summary>
        public async Task InsertAsync(Location location)
        {
            await _context.Locations.AddAsync(location);
            await _context.SaveChangesAsync();
        }

        #endregion
    }
}
