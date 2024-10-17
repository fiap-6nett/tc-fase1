using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using uBee.Domain.Entities;
using uBee.Persistence.Seeds;

namespace uBee.Persistence.Configurations
{
    public class LocationConfiguration : IEntityTypeConfiguration<Location>
    {
        public void Configure(EntityTypeBuilder<Location> builder)
        {
            builder.ToTable("locations");

            builder.HasKey(x => x.Id);

            builder.Property(x => x.Id)
                   .IsRequired();

            builder.Property(x => x.DDD)
                   .IsRequired();

            builder.Property(x => x.Region)
                   .HasMaxLength(100)
                   .IsRequired();

            //builder.HasData(new LocationSeed().Seed());
        }
    }
}
