using Microsoft.EntityFrameworkCore.Storage;

namespace uBee.Application.Core.Abstractions.Data
{
    public interface IUnitOfWork
    {
        #region IUnitOfWork Members

        Task<int> SaveChangesAsync(CancellationToken cancellationToken = default);
        Task<IDbContextTransaction> BeginTransactionAsync(CancellationToken cancellationToken = default);

        #endregion
    }
}
