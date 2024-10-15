using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using uBee.Domain.Entities;

namespace uBee.Persistence.Mappings
{
    public class HiveMapping : IEntityTypeConfiguration<Hive>
    {
        public void Configure(EntityTypeBuilder<Hive> builder)
        {
            builder.ToTable("hives");

            builder.HasKey(x => x.Id);

            builder.Property(x => x.Description)
                   .HasMaxLength(256)
                   .IsRequired();

            builder.Property(x => x.Status)
                   .IsRequired();

            builder.Property(x => x.IsDeleted)
                   .HasDefaultValue(false);

            builder.Property(x => x.CreatedAt)
                   .IsRequired();

            builder.Property(x => x.LastUpdatedAt);

            builder.HasOne(x => x.User)
                   .WithMany(u => u.Hives)
                   .HasForeignKey(x => x.IdUser)
                   .OnDelete(DeleteBehavior.Restrict);

            builder.HasQueryFilter(x => !x.IsDeleted);
        }
    }
}
