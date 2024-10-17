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

            builder.HasOne(x => x.Location)
                   .WithMany()
                   .HasForeignKey(x => x.IdLocation)
                   .OnDelete(DeleteBehavior.Restrict);

            builder.Property(x => x.IsDeleted)
                   .HasDefaultValue(false);

            builder.Property(x => x.CreatedAt)
                   .IsRequired();

            builder.Property(x => x.LastUpdatedAt);

            builder.HasQueryFilter(x => !x.IsDeleted);

            //SeedInitialUsers(builder);
        }

        private void SeedInitialUsers(EntityTypeBuilder<User> builder)
        {
            var users = new List<object>
            {
                 new { Id = Guid.NewGuid(), Name = "Administrador", Surname = "(built-in)",   Email = "admin@ubee.com",    Phone = "999999999", UserRole = EnUserRole.Administrator,  PasswordHash = "Admin@123", IdLocation = 1 },
                 new { Id = Guid.NewGuid(), Name = "Cleber",        Surname = "(built-in)",   Email = "cleber@ubee.com",   Phone = "999999999", UserRole = EnUserRole.Beekeeper,      PasswordHash = "Cleber@123", IdLocation = 2 },
                 new { Id = Guid.NewGuid(), Name = "Diego",         Surname = "(built-in)",   Email = "diego@ubee.com",    Phone = "999999999", UserRole = EnUserRole.Farmer,         PasswordHash = "Diego@123", IdLocation = 3 },
                 new { Id = Guid.NewGuid(), Name = "Lucas",         Surname = "(built-in)",   Email = "lucas@ubee.com",    Phone = "999999999", UserRole = EnUserRole.Farmer,         PasswordHash = "Lucas@123", IdLocation = 4 },
                 new { Id = Guid.NewGuid(), Name = "Rafael",        Surname = "(built-in)",   Email = "rafael@ubee.com",   Phone = "999999999", UserRole = EnUserRole.Beekeeper,      PasswordHash = "Rafael@123", IdLocation = 5 },
                 new { Id = Guid.NewGuid(), Name = "Wesley",        Surname = "(built-in)",   Email = "wesley@ubee.com",   Phone = "999999999", UserRole = EnUserRole.Farmer,         PasswordHash = "Wesley@123", IdLocation = 6 }
            };

            builder.HasData(users);
        }
    }
}
