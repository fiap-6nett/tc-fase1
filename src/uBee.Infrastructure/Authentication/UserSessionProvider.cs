using System.Security.Claims;
using Microsoft.AspNetCore.Http;
using uBee.Application.Core.Abstractions.Authentication;

namespace uBee.Infrastructure.Authentication
{
    internal sealed class UserSessionProvider : IUserSessionProvider
    {
        #region IUserSessionProvider Members

        public Guid IdUser { get; }

        #endregion

        #region Constructors

        public UserSessionProvider(IHttpContextAccessor httpContextAccessor)
        {
            if (!Guid.TryParse(httpContextAccessor.HttpContext?.User?.FindFirstValue(ClaimTypes.NameIdentifier), out var idUser))
                throw new ArgumentException("The user identifier claim is required.", nameof(httpContextAccessor));

            IdUser = idUser;
        }

        #endregion
    }
}
