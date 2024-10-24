using Microsoft.EntityFrameworkCore;
using uBee.Domain.Core.Primitives;
using uBee.Persistence.Core.Abstractions;

namespace uBee.Persistence.Core.Primitives
{
    internal abstract class EntitySeedConfiguration<TEntity, TKey> : IEntitySeedConfiguration
        where TEntity : Entity<TKey>
        where TKey : IEquatable<TKey>
    {
        #region IEntitySeedConfiguration Members

        /// <summary>
        /// Provides the seed data for the entity.
        /// </summary>
        /// <returns>An enumerable of objects representing the seed data.</returns>
        public abstract IEnumerable<object> Seed();

        /// <summary>
        /// Configures the model to include the seed data for the entity.
        /// </summary>
        /// <param name="modelBuilder">The model builder to configure the entity.</param>
        public void Configure(ModelBuilder modelBuilder)
            => modelBuilder.Entity<TEntity>().HasData(Seed());

        #endregion
    }
}
