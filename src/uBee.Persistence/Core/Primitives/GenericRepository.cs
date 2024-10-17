using System;
using System.Collections.Generic;
using System.Linq.Expressions;
using System.Threading.Tasks;
using Microsoft.EntityFrameworkCore;
using uBee.Domain.Core.Primitives;

namespace uBee.Persistence.Core.Primitives
{
    internal abstract class GenericRepository<TEntity>
        where TEntity : EntityBase
    {
        protected uBeeContext DbContext { get; }

        protected GenericRepository(uBeeContext dbContext)
            => DbContext = dbContext;

        // Operações Assíncronas Genéricas
        public async Task<TEntity> GetByIdAsync(Guid entityId)
            => await DbContext.Set<TEntity>().FindAsync(entityId);

        public async Task InsertAsync(TEntity entity)
            => await DbContext.Set<TEntity>().AddAsync(entity);

        public async Task InsertRangeAsync(IReadOnlyCollection<TEntity> entities)
            => await DbContext.Set<TEntity>().AddRangeAsync(entities);

        public async Task UpdateAsync(TEntity entity)
            => DbContext.Set<TEntity>().Update(entity);

        public async Task RemoveAsync(TEntity entity)
            => DbContext.Set<TEntity>().Remove(entity);

        public async Task<TEntity> FirstOrDefaultAsync(Expression<Func<TEntity, bool>> predicate)
            => await DbContext.Set<TEntity>().FirstOrDefaultAsync(predicate);

        public async Task<bool> AnyAsync(Expression<Func<TEntity, bool>> predicate)
            => await DbContext.Set<TEntity>().AnyAsync(predicate);
    }
}
