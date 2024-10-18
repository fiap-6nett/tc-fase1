namespace uBee.Application.Core.Abstractions.Authentication
{
    public interface IUserSessionProvider
    {
        #region IUserSessionProvider Members

        Guid IdUser { get; }

        #endregion
    }
}
