using Domain.Exceptions;
using Shared.ErrorModels;
using System.Net;

namespace Hotel_Reservation_System.Middelwares
{
    public class GlobalHandlingMiddelware
    {
        private readonly RequestDelegate _next;
        private readonly ILogger<GlobalHandlingMiddelware> _logger;

        public GlobalHandlingMiddelware(RequestDelegate next, ILogger<GlobalHandlingMiddelware> logger)
        {
            _next = next;
            _logger = logger;
        }
        public async Task InvokeAsync(HttpContext httpContext)
        {
            try
            {
                await _next(httpContext);
                if (httpContext.Response.StatusCode == (int)HttpStatusCode.NotFound)
                    await HandelNotFoundPoint(httpContext);
            }
            catch (Exception ex)
            {
                _logger.LogError(ex, "An unhandled exception occurred.");
                await HandelExeptionAsync(httpContext, ex);

            }
        }

        private async Task HandelNotFoundPoint(HttpContext httpContext)
        {
            httpContext.Response.ContentType = "application/json";
            var response = new ErrorDetailes
            {
                StatusCode = httpContext.Response.StatusCode,
                ErrorMessage = $"The EndPoint {httpContext.Request.Path} Not Found"
            }.ToString();
            await httpContext.Response.WriteAsync(response);
        }

        private async Task HandelExeptionAsync(HttpContext httpContext, Exception ex)
        {
            httpContext.Response.ContentType = "application/json";
            httpContext.Response.StatusCode = (int)HttpStatusCode.InternalServerError;

            var response = new ErrorDetailes
            {
                ErrorMessage = ex.Message,
            };

            httpContext.Response.StatusCode = ex switch
            {
                NotFoundException => (int)HttpStatusCode.NotFound,
                UnAuthorizedException => (int)HttpStatusCode.Unauthorized,
                ValidationException validationException => HandelValidationEx(validationException, response),
                _ => (int)HttpStatusCode.InternalServerError
            };

            response.StatusCode = httpContext.Response.StatusCode;

            await httpContext.Response.WriteAsync(response.ToString());
        }

        private int HandelValidationEx(ValidationException validationException, ErrorDetailes response)
        {
            response.Errors = validationException.Errors;
            return (int)HttpStatusCode.BadRequest;
        }
    }
}
