namespace InventoryManagement.API.Middleware;

public class UnauthorizedMessageMiddleware
{
    private readonly RequestDelegate _next;

    public UnauthorizedMessageMiddleware(RequestDelegate next)
    {
        _next = next;
    }

    public async Task InvokeAsync(HttpContext context)
    {
        // Temporarily capture the response
        var originalBody = context.Response.Body;
        using var newBody = new MemoryStream();
        context.Response.Body = newBody;

        await _next(context);  // Run the pipeline

        if (context.Response.StatusCode == StatusCodes.Status401Unauthorized)
        {
            context.Response.Body = originalBody;
            context.Response.ContentType = "application/json";

            var customResponse = new { message = "You are not login" };
            await context.Response.WriteAsJsonAsync(customResponse);
        }
        else
        {
            newBody.Seek(0, SeekOrigin.Begin);
            await newBody.CopyToAsync(originalBody);
        }
    }
}
