using Microsoft.Extensions.DependencyInjection;
using uBee.Application.Handlers.Users;
using uBee.Domain.Commands.Users;
using uBee.Domain.Queries.Users;
using uBee.Shared.Handlers.Contracts;

namespace uBee.Application
{
    public static class DependencyInjection
    {
        public static IServiceCollection AddApplication(this IServiceCollection services)
        {
            services.AddMediatR(cfg => cfg.RegisterServicesFromAssembly(AssemblyReference.Assembly));

            services.AddScoped<IHandlerCommand<InsertUserCommand>, InsertUserCommandHandler>();
            services.AddScoped<IHandlerCommand<UpdateUserCommand>, UpdateUserCommandHandler>();
            services.AddScoped<IHandlerQuery<GetUserByIdQuery>, GetUserByIdQueryHandler>();
            services.AddScoped<IHandlerQuery<GetUserByLocationQuery>, GetUserByLocationQueryHandler>();
            services.AddScoped<IHandlerCommand<MarkAsDeletedUserCommand>, MarkAsDeletedUserCommandHandler>();

            return services;
        }
    }
}
