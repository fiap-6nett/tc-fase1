using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using uBee.Domain.Repositories;
using uBee.Persistence.Repositories;

namespace uBee.Persistence
{
    public static class DependencyInjection
    {
        public static IServiceCollection AddPersistence(this IServiceCollection services, IConfiguration configuration)
        {
            var connectionString = configuration.GetConnectionString("uBeeDatabase");

            services.AddDbContext<uBeeContext>(options => options.UseSqlServer(connectionString));

            services.AddScoped<IUserRepository, UserRepository>();

            return services;
        }
    }
}
