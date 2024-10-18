using Microsoft.Extensions.DependencyInjection;
using uBee.Application.Core.Abstractions.Services;
using uBee.Application.Services;

namespace uBee.Application
{
    public static class DependencyInjection
    {
        public static IServiceCollection AddApplication(this IServiceCollection services)
        {
            services.AddMediatR(cfg => cfg.RegisterServicesFromAssembly(AssemblyReference.Assembly));

            services.AddScoped<IAuthenticationService, AuthenticationService>();
            services.AddScoped<IUserService, UserService>();

            return services;
        }
    }
}
