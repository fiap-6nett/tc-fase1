using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using uBee.Domain.Entities;
using uBee.Domain.ValueObjects;

namespace uBee.Persistence.Configurations
{
    public class UserConfiguration : IEntityTypeConfiguration<User>
    {
        public void Configure(EntityTypeBuilder<User> builder)
        {
            builder.ToTable("users");

            builder.HasKey(u => u.Id);

            builder.Property(u => u.Name)
                .IsRequired()
                .HasMaxLength(100);

            builder.Property(u => u.Surname)
                .IsRequired()
                .HasMaxLength(100);

            builder.OwnsOne(p => p.Email, builder =>
            {
                builder.WithOwner();
                builder.Property(email => email.Value)
                    .HasColumnName(nameof(User.Email))
                    .HasMaxLength(Email.MaxLength)
                    .IsRequired();
            });

            builder.OwnsOne(p => p.CPF, builder =>
            {
                builder.WithOwner();
                builder.Property(cpf => cpf.Value)
                    .HasColumnName(nameof(User.CPF))
                    .HasMaxLength(CPF.MaxLength)
                    .IsRequired();
            });

            builder.OwnsOne(p => p.Phone, builder =>
            {
                builder.WithOwner();
                builder.Property(phone => phone.Value)
                    .HasColumnName(nameof(User.Phone))
                    .HasMaxLength(Phone.MaxLength)
                    .IsRequired();
            });

            builder.Property("_passwordHash")
                .HasColumnName("PasswordHash")
                .IsRequired();

            builder.HasOne(u => u.Location)
                .WithMany(l => l.Users)
                .HasForeignKey(u => u.LocationId)
                .OnDelete(DeleteBehavior.Restrict);

            builder.Property(p => p.IsDeleted);
            builder.Property(p => p.CreatedAt).IsRequired();
            builder.Property(p => p.LastUpdatedAt);

            builder.HasQueryFilter(p => !p.IsDeleted);

            SeedBuiltInUsers(builder);
        }

        private void SeedBuiltInUsers(EntityTypeBuilder<User> builder)
        {
            var users = new List<(int Id, byte LocationId, string Name, string Surname, CPF Cpf, Email Email, Phone Phone, byte UserRole, string PasswordHash)>
            {
                (10_000, 1,     "Administrador",    "(built-in)",   CPF.Create("80455390037"), Email.Create("admin@ubee.com"),  Phone.Create("11-983594962"), 1, @"BGcEw9QQNyBOf+rLF/xrMboZKa035bzLBqgGpTBJTrE8Fk2TwAMbe8N49SbaM2Ro"), // Password: Admin@123
                (10_001, 1,     "Cleber",           "(built-in)",   CPF.Create("87622041068"), Email.Create("cleber@ubee.com"), Phone.Create("11-992504176"), 2, @"AxX8E7IFxv4rSTXU40IRjY6oPLVOq1y1tp0O5/vabDT/SPZlOWdktbiKCz2YLdzJ"), // Password: Cleber@123
                (10_002, 29,    "Diego",            "(built-in)",   CPF.Create("40070242003"), Email.Create("diego@ubee.com"),  Phone.Create("48-91662888"),  3, @"SlZEzmsPcuYfe8GRqN9lMLqv5KJpVmGpChaRoS5YVYQo/sSdeY6G5xj+nLF7zxJR"), // Password: Diego@123
                (10_003, 1,     "Lucas",            "(built-in)",   CPF.Create("99872134057"), Email.Create("lucas@ubee.com"),  Phone.Create("11-994635700"), 3, @"Wa+ZKmUcoWjcVPjQwVzY3tok2Thcejh2fGlA2lwZXv2oZ0NxL6Kb71NPYB8LP2De"), // Password: Lucas@123
                (10_004, 5,     "Rafael",           "(built-in)",   CPF.Create("46074925070"), Email.Create("rafael@ubee.com"), Phone.Create("15-998106370"), 2, @"tiNsfaj8kjCoJJcJeNyQqn03Ym4vuQldu3T+QL0AtJ9OzfkZcwo8UCd5+UcTDzEa"), // Password: Rafael@123
                (10_005, 4,     "Wesley",           "(built-in)",   CPF.Create("10096759070"), Email.Create("wesley@ubee.com"), Phone.Create("14-981343266"), 3, @"V8xyPoBEnUEUKLq5dxW5hqk8yiD42kfs1BMd8fKRkgrL9Ad1cA95US4avnA4TPYz")  // Password: Wesley@123
            };

            builder.HasData(users.Select(user => new
            {
                user.Id,
                user.Name,
                user.Surname,
                user.LocationId,
                user.UserRole,
                CreatedAt = DateTime.MinValue.Date,
                IsDeleted = false,
                _passwordHash = user.PasswordHash
            }));

            builder.OwnsOne(p => p.CPF).HasData(users.Select(user => new
            {
                UserId = user.Id,
                user.Cpf.Value
            }));

            builder.OwnsOne(p => p.Email).HasData(users.Select(user => new
            {
                UserId = user.Id,
                user.Email.Value
            }));

            builder.OwnsOne(p => p.Phone).HasData(users.Select(user => new
            {
                UserId = user.Id,
                user.Phone.Value
            }));
        }
    }
}
