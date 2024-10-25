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

            builder.HasOne(c => c.BeeContract)
                   .WithMany()
                   .HasForeignKey(c => c.IdBeeContract)
                   .OnDelete(DeleteBehavior.Restrict);

            builder.HasOne(c => c.Hive)
                   .WithMany()
                   .HasForeignKey(c => c.IdHive)
                   .OnDelete(DeleteBehavior.Restrict);

            builder.Property(p => p.IsDeleted);
            builder.Property(p => p.CreatedAt).IsRequired();
            builder.Property(p => p.LastUpdatedAt);

            builder.HasQueryFilter(p => !p.IsDeleted);
        }
    }
}
