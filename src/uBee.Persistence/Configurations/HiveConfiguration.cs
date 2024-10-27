using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using uBee.Domain.Entities;

namespace uBee.Persistence.Configurations
{
    public class HiveConfiguration : IEntityTypeConfiguration<Hive>
    {
        public void Configure(EntityTypeBuilder<Hive> builder)
        {
            builder.ToTable("hives");

            builder.HasKey(h => h.Id);

            builder.Property(h => h.Description)
                   .IsRequired()
                   .HasMaxLength(256);

            builder.Property(h => h.Status)
                   .IsRequired();

            builder.HasOne(h => h.User)
                   .WithMany(u => u.Hives)
                   .HasForeignKey(h => h.IdUser)
                   .OnDelete(DeleteBehavior.Restrict);

            builder.Property(p => p.IsDeleted);
            builder.Property(p => p.CreatedAt).IsRequired();
            builder.Property(p => p.LastUpdatedAt);

            builder.HasQueryFilter(p => !p.IsDeleted);
        }
    }
}
