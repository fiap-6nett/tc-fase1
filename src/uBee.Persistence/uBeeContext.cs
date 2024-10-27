using System.Data.SqlClient;
using System.Reflection;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.ChangeTracking;
using Microsoft.EntityFrameworkCore.Storage;
using uBee.Application.Core.Abstractions.Data;
using uBee.Domain.Core.Abstractions;
using uBee.Domain.Core.Primitives;
using uBee.Domain.Entities;

namespace uBee.Persistence
{
    public sealed class uBeeContext : DbContext, IDbContext, IUnitOfWork
    {
        #region Constructors

        public uBeeContext(DbContextOptions<uBeeContext> options)
            : base(options)
        { }

        #endregion

        #region DbSets

        public DbSet<User> Users { get; set; }
        public DbSet<Hive> Hives { get; set; }
        public DbSet<BeeContract> BeeContracts { get; set; }
        public DbSet<ContractedHive> ContractedHives { get; set; }
        public DbSet<Location> Locations { get; set; }

        #endregion

        #region IDbContext Members

        public DbSet<TEntity> Set<TEntity, TKey>()
            where TEntity : Entity<TKey>
            where TKey : IEquatable<TKey>
        {
            return base.Set<TEntity>();
        }

        public async Task<TEntity> GetBydIdAsync<TEntity, TKey>(TKey idEntity)
            where TEntity : Entity<TKey>
            where TKey : IEquatable<TKey>
        {
            return await Set<TEntity>().FirstOrDefaultAsync(e => e.Id.Equals(idEntity));
        }

        public void Insert<TEntity, TKey>(TEntity entity)
            where TEntity : Entity<TKey>
            where TKey : IEquatable<TKey>
        {
            Set<TEntity>().Add(entity);
        }

        public void InsertRange<TEntity, TKey>(IReadOnlyCollection<TEntity> entities)
            where TEntity : Entity<TKey>
            where TKey : IEquatable<TKey>
        {
            Set<TEntity>().AddRange(entities);
        }

        public void Remove<TEntity, TKey>(TEntity entity)
            where TEntity : Entity<TKey>
            where TKey : IEquatable<TKey>
        {
            Set<TEntity>().Remove(entity);
        }

        public async Task<int> ExecuteSqlAsync(string sql, IEnumerable<SqlParameter> parameters, CancellationToken cancellationToken = default)
        {
            return await Database.ExecuteSqlRawAsync(sql, parameters.ToArray(), cancellationToken);
        }

        #endregion

        #region IUnitOfWork Members

        public override async Task<int> SaveChangesAsync(CancellationToken cancellationToken = default)
        {
            var currentDate = DateTime.UtcNow;

            UpdateAuditableEntities(currentDate);
            HandleSoftDeletes();

            return await base.SaveChangesAsync(cancellationToken);
        }

        public Task<IDbContextTransaction> BeginTransactionAsync(CancellationToken cancellationToken = default)
            => Database.BeginTransactionAsync(cancellationToken);

        #endregion

        #region Overridden Methods

        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            // Apply all configurations from the assembly
            modelBuilder.ApplyConfigurationsFromAssembly(Assembly.GetExecutingAssembly());

            base.OnModelCreating(modelBuilder);
        }

        #endregion

        #region Private Methods

        /// <summary>
        /// Updates audit fields (CreatedAt and LastUpdatedAt) for entities that implement IAuditableEntity.
        /// </summary>
        private void UpdateAuditableEntities(DateTime currentDate)
        {
            foreach (var entityEntry in ChangeTracker.Entries<IAuditableEntity>())
            {
                if (entityEntry.State == EntityState.Added)
                    entityEntry.Property(nameof(IAuditableEntity.CreatedAt)).CurrentValue = currentDate;

                if (entityEntry.State == EntityState.Modified)
                    entityEntry.Property(nameof(IAuditableEntity.LastUpdatedAt)).CurrentValue = currentDate;
            }
        }

        /// <summary>
        /// Manages soft deletion for entities that implement ISoftDeletableEntity.
        /// </summary>
        private void HandleSoftDeletes()
        {
            foreach (var entityEntry in ChangeTracker.Entries<ISoftDeletableEntity>())
            {
                if (entityEntry.State == EntityState.Deleted)
                {
                    entityEntry.Property(nameof(ISoftDeletableEntity.IsDeleted)).CurrentValue = true;
                    entityEntry.State = EntityState.Modified;

                    UpdateDeletedEntityReferences(entityEntry);
                }
            }
        }

        /// <summary>
        /// Ensures that related entities to a deleted entity are not also deleted.
        /// </summary>
        private static void UpdateDeletedEntityReferences(EntityEntry entityEntry)
        {
            foreach (var referenceEntry in entityEntry.References.Where(r => r.TargetEntry.State == EntityState.Deleted))
            {
                referenceEntry.TargetEntry.State = EntityState.Unchanged;
                UpdateDeletedEntityReferences(referenceEntry.TargetEntry);
            }
        }

        #endregion

        #region Protected Methods

        protected override void OnConfiguring(DbContextOptionsBuilder optionsBuilder)
        {
            optionsBuilder.EnableSensitiveDataLogging();
        }

        #endregion
    }
}
