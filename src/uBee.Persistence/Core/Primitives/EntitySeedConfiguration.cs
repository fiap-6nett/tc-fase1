using Microsoft.EntityFrameworkCore;
using uBee.Domain.Core.Primitives;
using uBee.Persistence.Core.Abstractions;

namespace uBee.Persistence.Core.Primitives
{
    internal abstract class EntitySeedConfiguration<TEntity> : IEntitySeedConfiguration
        where TEntity : EntityBase
    {
        #region IEntitySeedConfiguration Members

        public abstract IEnumerable<object> Seed();

        public void Configure(ModelBuilder modelBuilder)
            => modelBuilder.Entity<TEntity>().HasData(Seed());

        #endregion
    }
}
