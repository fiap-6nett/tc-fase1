using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using uBee.Domain.Entities;

namespace uBee.Persistence.Configurations
{
    public class BeeContractConfiguration : IEntityTypeConfiguration<BeeContract>
    {
        public void Configure(EntityTypeBuilder<BeeContract> builder)
        {
            builder.ToTable("beecontracts");

            builder.HasKey(b => b.Id);

            builder.Property(b => b.StartDate)
                   .IsRequired();

            builder.Property(b => b.EndDate)
                   .IsRequired();

            builder.Property(b => b.Price)
                   .HasColumnType("decimal(18,2)")
                   .IsRequired();

            builder.Property(b => b.Status)
                   .IsRequired();

            builder.HasOne(b => b.User)
                   .WithMany(u => u.BeeContracts)
                   .HasForeignKey(b => b.IdUser)
                   .OnDelete(DeleteBehavior.Restrict);

            builder.Property(p => p.IsDeleted);
            builder.Property(p => p.CreatedAt).IsRequired();
            builder.Property(p => p.LastUpdatedAt);

            builder.HasQueryFilter(p => !p.IsDeleted);
        }
    }
}
