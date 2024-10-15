using Microsoft.EntityFrameworkCore;
using uBee.Domain.Entities;
using uBee.Domain.Repositories;

namespace uBee.Persistence.Repositories
{
    public class UserRepository : IUserRepository
    {
        private readonly uBeeContext _context;

        public UserRepository(uBeeContext context)
        {
            _context = context;
        }

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
            await _context.Users.AddAsync(user);
            await _context.SaveChangesAsync();
        }

        public async Task MarkAsDeleted(User user)
        {
            user.MarkAsDeleted();
            _context.Users.Update(user);
            await _context.SaveChangesAsync();
        }

        public async Task UpdateNameAsync(User user)
        {
            _context.Users.Update(user);
            await _context.SaveChangesAsync();
        }
    }
}
