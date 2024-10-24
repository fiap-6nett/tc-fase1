using uBee.Domain.Entities;
using uBee.Domain.Repositories;
using uBee.Persistence.Core.Primitives;
using Microsoft.EntityFrameworkCore;

namespace uBee.Persistence.Repositories
{
    internal sealed class UserRepository : GenericRepository<User, int>, IUserRepository
    {
        #region Private Fields

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
            => await FirstOrDefaultAsync(user => user.Email.Value == email);

        public async Task<bool> IsEmailUniqueAsync(string email)
            => !await AnyAsync(user => user.Email.Value == email);

        public async Task<IEnumerable<User>> GetByLocationAsync(int? dddNumber = null, string locationName = null)
        {
            IQueryable<User> query = _context.Users.Include(u => u.Location);

            if (dddNumber.HasValue)
            {
                query = query.Where(user => user.Location.Number == dddNumber.Value);
            }

            if (!string.IsNullOrEmpty(locationName))
            {
                query = query.Where(user => user.Location.Name == locationName);
            }

            return await query.ToListAsync();
        }

        #endregion
    }
}
