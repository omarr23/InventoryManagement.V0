using System.Text.Json;

namespace InventoryManagement.API.Middleware
{
    public class ExceptionHandlingMiddleware(RequestDelegate next, ILogger<ExceptionHandlingMiddleware> logger)
  {
        private readonly RequestDelegate _next = next;
        private readonly ILogger<ExceptionHandlingMiddleware> _logger = logger;

    public async Task InvokeAsync(HttpContext context)
        {
            try
            {
                await _next(context); // Continue processing the request
            }
            catch (Exception ex)
            {
                _logger.LogError(ex, "An unhandled exception occurred.");

                context.Response.StatusCode = StatusCodes.Status500InternalServerError;
                context.Response.ContentType = "application/json";

                var response = new
                {
                    error = "An unexpected error occurred.",
                    detail = ex.Message // You may omit this in production
                };

                var json = JsonSerializer.Serialize(response);
                await context.Response.WriteAsync(json);
            }
        }
    }
}
