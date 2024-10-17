using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using uBee.Domain.Entities;
using uBee.Domain.Enumerations;

namespace uBee.Persistence.Configurations
{
    public class UserConfiguration : IEntityTypeConfiguration<User>
    {
        public void Configure(EntityTypeBuilder<User> builder)
        {
            builder.ToTable("users");

            builder.HasKey(x => x.Id);

            builder.Property(x => x.Name)
                   .HasMaxLength(80)
                   .IsRequired();

            builder.Property(x => x.Surname)
                   .HasMaxLength(150)
                   .IsRequired();

            builder.Property(x => x.Email)
                   .HasMaxLength(150)
                   .IsRequired();

            builder.HasIndex(x => x.Email).IsUnique();

            builder.Property(x => x.Phone)
                   .HasMaxLength(15)
                   .IsRequired();

            builder.HasIndex(x => x.Phone).IsUnique();

            builder.Property<string>("_passwordHash")
                   .HasColumnName("PasswordHash")
                   .IsRequired();

            builder.Property(x => x.UserRole)
                   .IsRequired();

            builder.Property(x => x.Location)
                   .IsRequired();

            builder.Property(x => x.IsDeleted)
                   .HasDefaultValue(false);

            builder.Property(x => x.CreatedAt)
                   .HasDefaultValueSql("GETUTCDATE()")
                   .IsRequired();

            builder.Property(x => x.LastUpdatedAt);

            builder.HasQueryFilter(x => !x.IsDeleted);

            SeedInitialUsers(builder);
        }

        private void SeedInitialUsers(EntityTypeBuilder<User> builder)
        {
            var users = new List<object>
            {
                 new { Id = Guid.NewGuid(), Name = "Administrador", Surname = "(built-in)",   Email = "admin@ubee.com",    Phone = "999999999", UserRole = EnUserRole.Administrator,  _passwordHash = "Admin@123", Location = EnLocation.SaoPauloCity },
                 new { Id = Guid.NewGuid(), Name = "Cleber",        Surname = "(built-in)",   Email = "cleber@ubee.com",   Phone = "999999991", UserRole = EnUserRole.Beekeeper,      _passwordHash = "Cleber@123", Location = EnLocation.SorocabaRegion },
                 new { Id = Guid.NewGuid(), Name = "Diego",         Surname = "(built-in)",   Email = "diego@ubee.com",    Phone = "999999992", UserRole = EnUserRole.Farmer,         _passwordHash = "Diego@123", Location = EnLocation.SorocabaRegion },
                 new { Id = Guid.NewGuid(), Name = "Lucas",         Surname = "(built-in)",   Email = "lucas@ubee.com",    Phone = "999999993", UserRole = EnUserRole.Farmer,         _passwordHash = "Lucas@123", Location = EnLocation.SaoPauloCity },
                 new { Id = Guid.NewGuid(), Name = "Rafael",        Surname = "(built-in)",   Email = "rafael@ubee.com",   Phone = "999999994", UserRole = EnUserRole.Beekeeper,      _passwordHash = "Rafael@123", Location = EnLocation.SaoPauloCity },
                 new { Id = Guid.NewGuid(), Name = "Wesley",        Surname = "(built-in)",   Email = "wesley@ubee.com",   Phone = "999999995", UserRole = EnUserRole.Farmer,         _passwordHash = "Wesley@123", Location = EnLocation.RioDeJaneiroCity }
            };

            builder.HasData(users);
        }
    }
}
