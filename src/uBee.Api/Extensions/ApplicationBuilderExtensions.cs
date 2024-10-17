namespace uBee.Api.Extensions
{
    internal static class ApplicationBuilderExtensions
    {
        #region Extension Methods

        internal static IApplicationBuilder ConfigureSwagger(this IApplicationBuilder builder)
        {
            builder.UseSwagger();
            builder.UseSwaggerUI(swaggerUiOptions => swaggerUiOptions.SwaggerEndpoint("/swagger/v1/swagger.json", "uBee API"));

            return builder;
        }

        #endregion
    }
}
