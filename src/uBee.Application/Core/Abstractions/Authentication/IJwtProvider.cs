using uBee.Domain.Entities;

namespace uBee.Application.Core.Abstractions.Authentication
{
    public interface IJwtProvider
    {
        #region IJwtProvider Members

        string Create(User user);

        #endregion
    }
}
