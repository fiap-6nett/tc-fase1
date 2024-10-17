using Microsoft.EntityFrameworkCore;
using uBee.Domain.Entities;
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

        public async Task<bool> CheckEmailInUseAsync(string email)
        {
            return await _context.Users
                .AnyAsync(u => u.Email == email);
        }

        public async Task<User> GetByIdAsync(Guid idUser)
        {
            return await _context.Users
                .Include(u => u.Location)
                .FirstOrDefaultAsync(u => u.Id == idUser);
        }

        public async Task<IEnumerable<User>> GetByLocationAsync(int ddd)
        {
            return await _context.Users
                .Include(u => u.Location)
                .Where(u => u.Location.DDD == ddd)
                .ToListAsync();
        }

        public async Task InsertAsync(User user)
        {
            await base.InsertAsync(user);
            await _context.SaveChangesAsync();
        }

        public async Task MarkAsDeleted(User user)
        {
            user.MarkAsDeleted();
            await base.UpdateAsync(user);
            await _context.SaveChangesAsync();
        }

        public async Task UpdateNameAsync(User user)
        {
            await base.UpdateAsync(user);
            await _context.SaveChangesAsync();
        }

        #endregion
    }
}
