using System.Data.SqlClient;
using Microsoft.EntityFrameworkCore;
using uBee.Domain.Core.Primitives;

namespace uBee.Application.Core.Abstractions.Data
{
    public interface IDbContext
    {
        #region IDbContext Members

        DbSet<TEntity> Set<TEntity, TKey>()
            where TEntity : EntityBase
            where TKey : IEquatable<TKey>;

        Task<TEntity> GetBydIdAsync<TEntity, TKey>(TKey idEntity)
            where TEntity : EntityBase
            where TKey : IEquatable<TKey>;

        void Insert<TEntity, TKey>(TEntity entity)
            where TEntity : EntityBase
            where TKey : IEquatable<TKey>;

        void InsertRange<TEntity, TKey>(IReadOnlyCollection<TEntity> entities)
            where TEntity : EntityBase
            where TKey : IEquatable<TKey>;

        void Remove<TEntity, TKey>(TEntity entity)
            where TEntity : EntityBase
            where TKey : IEquatable<TKey>;

        Task<int> ExecuteSqlAsync(string sql, IEnumerable<SqlParameter> parameters, CancellationToken cancellationToken = default);

        #endregion
    }
}
