using uBee.Domain.Entities;

namespace uBee.Domain.Repositories
{
    public interface IUserRepository
    {
        #region IUserRepository Members

        Task<User> GetByIdAsync(Guid idUser);
        Task<IEnumerable<User>> GetByLocationAsync(int ddd);
        Task<bool> CheckEmailInUseAsync(string email);
        Task InsertAsync(User user);
        Task UpdateNameAsync(User user);
        Task MarkAsDeleted(User user);

        #endregion
    }
}
