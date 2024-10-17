using uBee.Domain.Entities;
using uBee.Domain.Enumerations;

namespace uBee.Domain.Repositories
{
    public interface IUserRepository
    {
        #region IUserRepository Members

        Task<User> GetByIdAsync(Guid idUser);
        Task<IEnumerable<User>> GetByLocationAsync(EnLocation ddd);
        Task<bool> CheckEmailInUseAsync(string email);
        Task InsertAsync(User user);
        Task UpdateNameAsync(User user);
        Task MarkAsDeleted(User user);

        #endregion
    }
}
