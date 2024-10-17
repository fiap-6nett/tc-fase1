using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using uBee.Application.Core.Abstractions.Data;
using uBee.Domain.Repositories;
using uBee.Persistence.Infrastructure;
using uBee.Persistence.Repositories;

namespace uBee.Persistence
{
    public static class DependencyInjection
    {
        public static IServiceCollection AddPersistence(this IServiceCollection services, IConfiguration configuration)
        {
            var connectionString = configuration.GetConnectionString(ConnectionString.SettingsKey);
            services.AddDbContext<uBeeContext>(options => options.UseSqlServer(connectionString));

            services.AddScoped<IDbContext>(serviceProvider => serviceProvider.GetRequiredService<uBeeContext>());
            services.AddScoped<IUnitOfWork>(serviceProvider => serviceProvider.GetRequiredService<uBeeContext>());

            services.AddScoped<IUserRepository, UserRepository>();

            return services;
        }
    }
}
