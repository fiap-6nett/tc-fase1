using uBee.Application.Contracts.Authentication;
using uBee.Application.Contracts.Common;
using uBee.Application.Contracts.Users;
using uBee.Domain.Enumerations;

namespace uBee.Application.Core.Abstractions.Services
{
    public interface IUserService
    {
        #region IUserService Members

        Task<DetailedUserResponse> GetUserByIdAsync(Guid idUser);
        Task<PagedList<UserResponse>> GetUsersAsync(GetUsersRequest request);
        Task<TokenResponse> CreateAsync(string name, string surname, string email, string password, EnUserRole userRole, EnLocation location, string phone);
        Task ChangePasswordAsync(Guid idUser, string password);
        Task UpdateUserAsync(Guid idUser, string name, string surname);

        #endregion
    }
}
