using Flunt.Notifications;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.ChangeTracking;
using Microsoft.EntityFrameworkCore.Storage;
using System.Reflection;
using uBee.Domain.Entities;
using uBee.Domain.Core.Abstractions;
using uBee.Persistence.Mappings;
using System.Threading;
using System.Threading.Tasks;

namespace uBee.Persistence
{
    public class uBeeContext : DbContext
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
        public DbSet<Location> Locations { get; set; }
        public DbSet<ContractedHive> ContractedHives { get; set; }

        #endregion

        #region Overridden Methods

        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            modelBuilder.Ignore<Notification>();

            modelBuilder.ApplyConfiguration(new UserMapping());
            modelBuilder.ApplyConfiguration(new HiveMapping());
            modelBuilder.ApplyConfiguration(new BeeContractMapping());
            modelBuilder.ApplyConfiguration(new LocationMapping());
            modelBuilder.ApplyConfiguration(new ContractedHiveMapping());

            base.OnModelCreating(modelBuilder);
        }

        public override async Task<int> SaveChangesAsync(CancellationToken cancellationToken = default)
        {
            var currentDate = DateTime.UtcNow;

            UpdateAuditableEntities(currentDate);
            HandleSoftDeletes();

            return await base.SaveChangesAsync(cancellationToken);
        }

        #endregion

        #region Private Methods

        /// <summary>
        /// Updates the audit fields (CreatedAt and LastUpdatedAt) for entities that implement IAuditableEntity.
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
        /// Automatically handles soft deletion for entities that implement ISoftDeletableEntity.
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
        /// Ensures that related entities of a soft-deleted entity are not deleted as well.
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
    }
}
