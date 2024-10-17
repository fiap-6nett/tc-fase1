using Microsoft.EntityFrameworkCore.Metadata.Builders;
using Microsoft.EntityFrameworkCore;
using uBee.Domain.Entities;

namespace uBee.Persistence.Configurations
{
    public class BeeContractConfiguration : IEntityTypeConfiguration<BeeContract>
    {
        public void Configure(EntityTypeBuilder<BeeContract> builder)
        {
            builder.ToTable("beecontracts");

            builder.HasKey(x => x.Id);

            builder.Property(x => x.StartDate)
                   .IsRequired();

            builder.Property(x => x.EndDate)
                   .IsRequired();

            builder.Property(x => x.Price)
                   .HasColumnType("decimal(18,2)")
                   .IsRequired();

            builder.Property(x => x.Status)
                   .IsRequired();

            builder.Property(x => x.IsDeleted)
                   .HasDefaultValue(false);

            builder.Property(x => x.CreatedAt)
                   .IsRequired();

            builder.Property(x => x.LastUpdatedAt);

            builder.HasOne(x => x.User)
                   .WithMany(u => u.BeeContracts)
                   .HasForeignKey(x => x.IdUser)
                   .OnDelete(DeleteBehavior.Restrict);

            builder.HasQueryFilter(x => !x.IsDeleted);
        }
    }
}
