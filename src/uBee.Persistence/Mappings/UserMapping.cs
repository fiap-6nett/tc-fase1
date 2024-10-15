using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using uBee.Domain.Entities;
using uBee.Domain.Enumerations;

namespace uBee.Persistence.Mappings
{
    public class UserMapping : IEntityTypeConfiguration<User>
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

            SeedInitialUsers(builder);
        }

        private void SeedInitialUsers(EntityTypeBuilder<User> builder)
        {
            var users = new List<(Guid Id, string Name, string Surname, string Email, string Phone, EnUserRole UserRole, string PasswordHash)>
            {
                 new (Guid.NewGuid(), "Administrador",  "(built-in)",   "admin@ubee.com",    "999999999", EnUserRole.Administrator,  "Admin@123"),
                 new (Guid.NewGuid(), "Cleber",         "(built-in)",   "cleber@ubee.com",   "999999999", EnUserRole.Beekeeper,      "Cleber@123"),
                 new (Guid.NewGuid(), "Diego",          "(built-in)",   "diego@ubee.com",    "999999999", EnUserRole.Farmer,         "Diego@123"),
                 new (Guid.NewGuid(), "Lucas",          "(built-in)",   "lucas@ubee.com",    "999999999", EnUserRole.Farmer,         "Lucas@123"),
                 new (Guid.NewGuid(), "Rafael",         "(built-in)",   "rafael@ubee.com",   "999999999", EnUserRole.Beekeeper,      "Rafael@123"),
                 new (Guid.NewGuid(), "Wesley",         "(built-in)",   "wesley@ubee.com",   "999999999", EnUserRole.Farmer,         "Wesley@123")
            };

            builder.HasData(users.Select(user => new
            {
                user.Id,
                user.Name,
                user.Surname,
                user.Email,
                user.Phone,
                user.PasswordHash,
                user.UserRole,
                CreatedAt = DateTime.UtcNow,
                IsDeleted = false
            }));
        }
    }
}
