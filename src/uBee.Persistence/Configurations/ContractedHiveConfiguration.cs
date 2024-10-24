using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using uBee.Domain.Entities;

namespace uBee.Persistence.Configurations
{
    public class ContractedHiveConfiguration : IEntityTypeConfiguration<ContractedHive>
    {
        public void Configure(EntityTypeBuilder<ContractedHive> builder)
        {
            builder.ToTable("contractedhives");

            builder.HasKey(c => c.Id);

            builder.Property(c => c.CreatedAt)
                   .IsRequired();

            builder.Property(c => c.LastUpdatedAt)
                   .IsRequired(false);

            builder.HasOne(c => c.BeeContract)
                   .WithMany()
                   .HasForeignKey(c => c.IdBeeContract)
                   .OnDelete(DeleteBehavior.Restrict);

            builder.HasOne(c => c.Hive)
                   .WithMany()
                   .HasForeignKey(c => c.IdHive)
                   .OnDelete(DeleteBehavior.Restrict);

            builder.Property(c => c.IsDeleted)
                   .HasDefaultValue(false);

            builder.HasQueryFilter(c => !c.IsDeleted);
        }
    }
}
