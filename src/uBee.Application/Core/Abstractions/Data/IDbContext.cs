using System.Data.SqlClient;
using Microsoft.EntityFrameworkCore;
using uBee.Domain.Core.Primitives;

namespace uBee.Application.Core.Abstractions.Data
{
    public interface IDbContext
    {
        #region IDbContext Members

        /// <summary>
        /// Returns a DbSet of the specified entity, allowing queries and manipulations in the context.
        /// </summary>
        /// <typeparam name="TEntity">The type of the entity.</typeparam>
        /// <typeparam name="TKey">The type of the entity's primary key.</typeparam>
        /// <returns>The DbSet of the entity.</returns>
        DbSet<TEntity> Set<TEntity, TKey>()
            where TEntity : Entity<TKey>
            where TKey : IEquatable<TKey>;

        /// <summary>
        /// Retrieves an entity by its ID.
        /// </summary>
        /// <typeparam name="TEntity">The type of the entity.</typeparam>
        /// <typeparam name="TKey">The type of the entity's primary key.</typeparam>
        /// <param name="idEntity">The ID of the entity.</param>
        /// <returns>The found entity or null.</returns>
        Task<TEntity> GetBydIdAsync<TEntity, TKey>(TKey idEntity)
            where TEntity : Entity<TKey>
            where TKey : IEquatable<TKey>;

        /// <summary>
        /// Inserts a new entity into the context.
        /// </summary>
        /// <typeparam name="TEntity">The type of the entity.</typeparam>
        /// <typeparam name="TKey">The type of the entity's primary key.</typeparam>
        /// <param name="entity">The entity to be inserted.</param>
        void Insert<TEntity, TKey>(TEntity entity)
            where TEntity : Entity<TKey>
            where TKey : IEquatable<TKey>;

        /// <summary>
        /// Inserts a collection of entities into the context.
        /// </summary>
        /// <typeparam name="TEntity">The type of the entities.</typeparam>
        /// <typeparam name="TKey">The type of the entities' primary key.</typeparam>
        /// <param name="entities">The collection of entities to be inserted.</param>
        void InsertRange<TEntity, TKey>(IReadOnlyCollection<TEntity> entities)
            where TEntity : Entity<TKey>
            where TKey : IEquatable<TKey>;

        /// <summary>
        /// Removes an entity from the context.
        /// </summary>
        /// <typeparam name="TEntity">The type of the entity.</typeparam>
        /// <typeparam name="TKey">The type of the entity's primary key.</typeparam>
        /// <param name="entity">The entity to be removed.</param>
        void Remove<TEntity, TKey>(TEntity entity)
            where TEntity : Entity<TKey>
            where TKey : IEquatable<TKey>;

        /// <summary>
        /// Executes a raw SQL query in the context.
        /// </summary>
        /// <param name="sql">The SQL command.</param>
        /// <param name="parameters">The SQL parameters.</param>
        /// <param name="cancellationToken">The cancellation token.</param>
        /// <returns>The number of rows affected.</returns>
        Task<int> ExecuteSqlAsync(string sql, IEnumerable<SqlParameter> parameters, CancellationToken cancellationToken = default);

        #endregion
    }
}
