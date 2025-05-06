using System.Text.Json;
using InventoryManagement.BLL.Exceptions;
using Microsoft.AspNetCore.Http;
using Microsoft.Extensions.Hosting;
using Microsoft.Extensions.Logging;

namespace InventoryManagement.API.Middleware
{
    public class ExceptionHandlingMiddleware
    {
        private readonly RequestDelegate _next;
        private readonly ILogger<ExceptionHandlingMiddleware> _logger;
        private readonly IHostEnvironment _env;

        public ExceptionHandlingMiddleware(RequestDelegate next, ILogger<ExceptionHandlingMiddleware> logger, IHostEnvironment env)
        {
            _next = next;
            _logger = logger;
            _env = env;
        }

        public async Task InvokeAsync(HttpContext context)
        {
            try
            {
                await _next(context);
            }
            catch (Exception ex)
            {
                _logger.LogError(ex, "An unhandled exception occurred");

                var response = context.Response;
                response.ContentType = "application/json";

                var errorResponse = new
                {
                    status = ex is BaseAppException baseStatusEx ? baseStatusEx.StatusCode : StatusCodes.Status500InternalServerError,
                    errorCode = ex is BaseAppException baseErrorCodeEx ? baseErrorCodeEx.ErrorCode : "INTERNAL_SERVER_ERROR",
                    message = ex.Message,
                    errors = ex is BaseAppException baseErrorsEx ? baseErrorsEx.Errors : null,
                    detail = _env.IsDevelopment() ? ex.StackTrace : null
                };

                response.StatusCode = errorResponse.status;
                var json = JsonSerializer.Serialize(errorResponse);
                await response.WriteAsync(json);
            }
        }
    }
}