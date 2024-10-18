using uBee.Domain.Entities;
using uBee.Domain.Enumerations;
using uBee.Domain.Repositories;
using uBee.Persistence.Core.Primitives;

namespace uBee.Persistence.Repositories
{
    internal sealed class UserRepository : GenericRepository<User>, IUserRepository
    {
        #region Fields

        private readonly uBeeContext _context;

        #endregion

        #region Constructors

        public UserRepository(uBeeContext context) : base(context)
        {
            _context = context;
        }

        #endregion

        #region IUserRepository Members

        public async Task<User> GetByEmailAsync(string email)
            => await FirstOrDefaultAsync(user => user.Email == email);

        public async Task<bool> IsEmailUniqueAsync(string email)
            => !await AnyAsync(user => user.Email == email);

        public async Task<IEnumerable<User>> GetByLocationAsync(EnLocation ddd)
            => (IEnumerable<User>)await FirstOrDefaultAsync(user => user.Location == ddd);

        #endregion
    }
}
