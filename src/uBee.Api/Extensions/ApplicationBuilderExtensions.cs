namespace uBee.Api.Extensions
{
    internal static class ApplicationBuilderExtensions
    {
        #region Extension Methods

        internal static IApplicationBuilder ConfigureSwagger(this IApplicationBuilder builder)
        {
            var env = builder.ApplicationServices.GetRequiredService<IWebHostEnvironment>();

            if (env.IsDevelopment())
            {
                builder.UseDeveloperExceptionPage();

                builder.UseSwagger();

                builder.UseSwaggerUI(swaggerUiOptions =>
                {
                    swaggerUiOptions.SwaggerEndpoint("/swagger/v1/swagger.json", "uBee.API v1");
                    swaggerUiOptions.RoutePrefix = string.Empty;
                });
            }

            return builder;
        }

        #endregion
    }
}
