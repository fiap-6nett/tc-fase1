using System;
using System.Collections.Generic;
using System.Linq.Expressions;
using System.Threading.Tasks;
using Microsoft.EntityFrameworkCore;
using uBee.Domain.Core.Primitives;

namespace uBee.Persistence.Core.Primitives
{
    internal abstract class GenericRepository<TEntity, TKey>
        where TEntity : Entity<TKey>
        where TKey : IEquatable<TKey>
    {
        #region Constructors

        protected uBeeContext DbContext { get; }

        protected GenericRepository(uBeeContext dbContext)
            => DbContext = dbContext;

        #endregion

        #region CRUD Operations

        public async Task<TEntity> GetByIdAsync(TKey entityId)
            => await DbContext.Set<TEntity>().FindAsync(entityId);

        public async Task InsertAsync(TEntity entity)
            => await DbContext.Set<TEntity>().AddAsync(entity);

        public async Task InsertRangeAsync(IReadOnlyCollection<TEntity> entities)
            => await DbContext.Set<TEntity>().AddRangeAsync(entities);

        public Task UpdateAsync(TEntity entity)
        {
            DbContext.Set<TEntity>().Update(entity);
            return Task.CompletedTask;
        }

        public Task RemoveAsync(TEntity entity)
        {
            DbContext.Set<TEntity>().Remove(entity);
            return Task.CompletedTask;
        }

        #endregion

        #region Query Operations

        public async Task<TEntity> FirstOrDefaultAsync(Expression<Func<TEntity, bool>> predicate)
            => await DbContext.Set<TEntity>().FirstOrDefaultAsync(predicate);

        public async Task<bool> AnyAsync(Expression<Func<TEntity, bool>> predicate)
            => await DbContext.Set<TEntity>().AnyAsync(predicate);

        #endregion
    }
}
