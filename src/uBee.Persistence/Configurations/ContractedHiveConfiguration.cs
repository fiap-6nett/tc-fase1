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

            builder.HasKey(x => x.Id);

            builder.Property(x => x.CreatedAt)
                   .IsRequired();

            builder.Property(x => x.LastUpdatedAt);

            builder.HasOne(x => x.BeeContract)
                   .WithMany()
                   .HasForeignKey(x => x.IdBeeContract)
                   .OnDelete(DeleteBehavior.Restrict);

            builder.HasOne(x => x.Hive)
                   .WithMany()
                   .HasForeignKey(x => x.IdHive)
                   .OnDelete(DeleteBehavior.Restrict);

            builder.Property(x => x.IsDeleted)
                   .HasDefaultValue(false);

            builder.HasQueryFilter(x => !x.IsDeleted);
        }
    }
}
