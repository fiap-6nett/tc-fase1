using uBee.Api.Contracts;
using uBee.Domain.Core.Primitives;
using uBee.Domain.Errors;
using uBee.Domain.Exceptions;
using uBee.Application.Core.Exceptions;
using System.Net;
using System.Text.Json;

namespace uBee.Api.Middlewares
{
    internal class ExceptionHandlerMiddleware
    {
        #region Read-Only Fields

        private readonly RequestDelegate _next;
        private readonly ILogger<ExceptionHandlerMiddleware> _logger;

        #endregion

        #region Constructors

        public ExceptionHandlerMiddleware(RequestDelegate next, ILogger<ExceptionHandlerMiddleware> logger)
        {
            _next = next ?? throw new ArgumentNullException(nameof(next));
            _logger = logger ?? throw new ArgumentNullException(nameof(logger));
        }

        #endregion

        #region Public Methods

        public async Task Invoke(HttpContext httpContext)
        {
            try
            {
                await _next(httpContext);
            }
            catch (Exception ex)
            {
                _logger.LogError(ex, "An exception occurred: {Message}", ex.Message);
                await HandleExceptionAsync(httpContext, ex);
            }
        }

        #endregion

        #region Private Methods

        private static async Task HandleExceptionAsync(HttpContext httpContext, Exception exception)
        {
            (HttpStatusCode httpStatusCode, IReadOnlyCollection<Error> errors) = GetHttpStatusCodeAndErrors(exception);

            httpContext.Response.ContentType = "application/json";
            httpContext.Response.StatusCode = (int)httpStatusCode;

            var serializerOptions = new JsonSerializerOptions
            {
                PropertyNamingPolicy = JsonNamingPolicy.CamelCase
            };

            var response = JsonSerializer.Serialize(new ApiErrorResponse(errors), serializerOptions);

            await httpContext.Response.WriteAsync(response);
        }

        private static (HttpStatusCode httpStatusCode, IReadOnlyCollection<Error>) GetHttpStatusCodeAndErrors(Exception exception)
        {
            return exception switch
            {
                ValidationException validationException => (HttpStatusCode.BadRequest, validationException.Errors),
                InvalidPermissionException invalidPermissionException => (HttpStatusCode.Forbidden, new[] { invalidPermissionException.Error }),
                NotFoundException notFoundException => (HttpStatusCode.NotFound, new[] { notFoundException.Error }),
                DomainException domainException => (HttpStatusCode.BadRequest, new[] { domainException.Error }),
                _ => (HttpStatusCode.InternalServerError, new[] { DomainError.General.ServerError })
            };
        }

        #endregion
    }
}
