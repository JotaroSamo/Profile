using System.Net;
using System.Text.Json;
using profile_Domain.Exception;
using profile_Domain.Exception.Base;
using profile_Domain.Exception.Model;

namespace profile_API.Middleware
{
    public class ExceptionHandlingMiddleware
    {
        private readonly RequestDelegate _next;

        public ExceptionHandlingMiddleware(RequestDelegate next)
        {
            _next = next;
        }

        public async Task InvokeAsync(HttpContext context)
        {
            try
            {
                await _next(context);
            }
            catch (ProfileExceptionBase ex)
            {
                await HandleExceptionAsync(context, ex);
            }
            catch (Exception ex)
            {
                await HandleExceptionAsync(context, new ProfileException(500, ex.Message));
            }
        }

        private static Task HandleExceptionAsync(HttpContext context, ProfileExceptionBase exception)
        {
            context.Response.ContentType = "application/json";
            context.Response.StatusCode = exception.StatusCode;

            var result = JsonSerializer.Serialize(new ErrorModel()
            {
                StatusCode = exception.StatusCode,
                ErrorMessage = exception.ErrorMessage
            });
            return context.Response.WriteAsync(result);
        }
    }
}

