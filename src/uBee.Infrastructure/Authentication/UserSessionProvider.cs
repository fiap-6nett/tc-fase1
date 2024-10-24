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
            var userIdClaim = httpContextAccessor.HttpContext?.User?.FindFirstValue(ClaimTypes.NameIdentifier);
            if (!Guid.TryParse(userIdClaim, out var idUser))
            {
                throw new ArgumentException("The user identifier claim is required.", nameof(httpContextAccessor));
            }

            IdUser = idUser;
        }

        #endregion
    }
}
