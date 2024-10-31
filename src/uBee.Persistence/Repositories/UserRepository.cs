using Microsoft.EntityFrameworkCore;
using uBee.Application.Repositories;
using uBee.Domain.Entities;
using uBee.Persistence.Core.Primitives;

namespace uBee.Persistence.Repositories
{
    internal sealed class UserRepository : GenericRepository<User, int>, IUserRepository
    {
        #region Private Fields

        private readonly uBeeContext _context;

        #endregion

        #region Constructors

        /// <summary>
        /// Initializes a new instance of the <see cref="UserRepository"/> class.
        /// </summary>
        /// <param name="context">The database context to be used by the repository.</param>
        public UserRepository(uBeeContext context) : base(context)
        {
            _context = context;
        }

        #endregion

        #region IUserRepository Members

        /// <summary>
        /// Retrieves a user by their email.
        /// </summary>
        /// <param name="email">The user's email.</param>
        /// <returns>The user entity if found, otherwise null.</returns>
        public async Task<User> GetByEmailAsync(string email)
            => await FirstOrDefaultAsync(user => user.Email.Value == email);

        /// <summary>
        /// Checks if an email is unique within the system.
        /// </summary>
        /// <param name="email">The email to check.</param>
        /// <returns>True if the email is unique, false otherwise.</returns>
        public async Task<bool> IsEmailUniqueAsync(string email)
            => !await AnyAsync(user => user.Email.Value == email);

        /// <summary>
        /// Checks if a Cpf is unique within the system.
        /// </summary>
        /// <param name="cpf">The Cpf to check.</param>
        /// <returns>True if the Cpf is unique, false otherwise.</returns>
        public async Task<bool> IsCpfUniqueAsync(string cpf)
            => !await AnyAsync(user => user.CPF.Value == cpf);

        /// <summary>
        /// Checks if a phone number is unique within the system.
        /// </summary>
        /// <param name="phone">The phone number to check.</param>
        /// <returns>True if the phone number is unique, false otherwise.</returns>
        public async Task<bool> IsPhoneUniqueAsync(string phone)
            => !await AnyAsync(user => user.Phone.Value == phone);

        /// <summary>
        /// Retrieves users by their location, filtered by DDD number or location name.
        /// </summary>
        /// <param name="dddNumber">The DDD number to filter by (optional).</param>
        /// <param name="locationName">The location name to filter by (optional).</param>
        /// <returns>A list of users matching the specified criteria.</returns>
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
