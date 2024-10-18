using uBee.Domain.Entities;
using uBee.Domain.Enumerations;

namespace uBee.Domain.Repositories
{
    public interface IUserRepository
    {
        #region IUserRepository Members

        Task<User> GetByIdAsync(Guid idUser);
        Task<User> GetByEmailAsync(string email);
        Task<bool> IsEmailUniqueAsync(string email);
        Task<IEnumerable<User>> GetByLocationAsync(EnLocation ddd);
        Task InsertAsync(User user);

        #endregion
    }
}
