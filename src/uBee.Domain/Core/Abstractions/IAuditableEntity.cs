namespace uBee.Domain.Core.Abstractions
{
    public interface IAuditableEntity
    {
        #region IAuditableEntity Members
        DateTime CreatedAt { get; }
        DateTime? LastUpdatedAt { get; }

        #endregion
    }
}
