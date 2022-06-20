using LitApp.BLL.Exceptions;
using Microsoft.AspNetCore.Http;
using Microsoft.Extensions.Logging;
using System.Net;
using System.Text.Json;
using System.Threading.Tasks;

namespace LitApp.PL.Middleware
{
    public class ErrorHandlerMiddleware
    {
        private readonly RequestDelegate _next;
        private readonly ILogger<ErrorHandlerMiddleware> _logger;

        public ErrorHandlerMiddleware(RequestDelegate next, ILogger<ErrorHandlerMiddleware> logger)
        {
            _next = next;
            _logger = logger;
        }

        public async Task Invoke(HttpContext context)
        {
            try
            {
                await _next(context);
            }
            catch (AppException error)
            {
                await HandleExceptionAsync(context, error.StackTrace, HttpStatusCode.BadRequest, error.Message);
            }
            catch (InternalServerException error)
            {
                await HandleExceptionAsync(context, error.StackTrace, HttpStatusCode.InternalServerError, error.Message);
            }
        }

        private async Task HandleExceptionAsync(HttpContext context, string exMessage, HttpStatusCode httpStatusCode, string message)
        {
            _logger.LogError(exMessage);

            HttpResponse response = context.Response;

            response.ContentType = "application/json";

            response.StatusCode = (int)httpStatusCode;

            var error = new
            {
                Message = message,
                StatusCode = (int)httpStatusCode,
            };

            string result = JsonSerializer.Serialize(error);

            await response.WriteAsJsonAsync(result);
        }
    }
}
