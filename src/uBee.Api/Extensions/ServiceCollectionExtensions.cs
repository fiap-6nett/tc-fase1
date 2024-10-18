using Microsoft.EntityFrameworkCore;
using Microsoft.OpenApi.Models;
using Swashbuckle.AspNetCore.Filters;
using uBee.Persistence;

namespace uBee.Api.Extensions
{
    internal static class ServiceCollectionExtensions
    {
        #region Extension Methods

        internal static IServiceCollection AddSwagger(this IServiceCollection services)
        {
            services.AddSwaggerGen(setup =>
            {
                setup.SwaggerDoc("v1", new OpenApiInfo
                {
                    Title = "uBee - API",
                    Version = "v1"
                });

                setup.AddSecurityDefinition("Bearer", new OpenApiSecurityScheme()
                {
                    Name = "Authorization",
                    Description = "JWT Authorization header using the Bearer scheme.",
                    In = ParameterLocation.Header,
                    Type = SecuritySchemeType.ApiKey,
                    BearerFormat = "JWT",
                    Scheme = "Bearer",
                });

                setup.AddSecurityRequirement(new OpenApiSecurityRequirement
                {
                    {
                        new OpenApiSecurityScheme
                        {
                            Name = "Bearer",
                            In = ParameterLocation.Header,
                            Reference = new OpenApiReference
                            {
                                Id = "Bearer",
                                Type = ReferenceType.SecurityScheme
                            }
                        },
                        Array.Empty<string>()
                    }
                });

                setup.OperationFilter<SecurityRequirementsOperationFilter>(true, "Bearer");
            });

            return services;
        }

        internal static IApplicationBuilder EnsureDatabaseCreated(this IApplicationBuilder builder)
        {
            using var serviceScope = builder.ApplicationServices.CreateScope();
            using var dbContext = serviceScope.ServiceProvider.GetRequiredService<uBeeContext>();

            dbContext.Database.Migrate();

            return builder;
        }

        #endregion
    }
}
