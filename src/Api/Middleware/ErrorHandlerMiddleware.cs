using System.Net;
using System.Text.Json;
using Microsoft.Extensions.Logging;

namespace Api.Middleware;

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
        _logger.LogInformation("Incoming request: {Method} {Path}", context.Request.Method, context.Request.Path);

        try
        {
            await _next(context);
        }
        catch (ArgumentException ex)
        {
            _logger.LogWarning("Validation error: {Message}", ex.Message);
            await Handle(context, ex.Message, HttpStatusCode.BadRequest);
        }
        catch (KeyNotFoundException ex)
        {
            _logger.LogWarning("Resource not found: {Message}", ex.Message);
            await Handle(context, ex.Message, HttpStatusCode.NotFound);
        }
        catch (Exception ex)
        {
            _logger.LogError(ex, "Unhandled exception occurred");
            await Handle(context, "Something went wrong", HttpStatusCode.InternalServerError);
        }
    }

    private static async Task Handle(HttpContext context, string message, HttpStatusCode status)
    {
        context.Response.ContentType = "application/json";
        context.Response.StatusCode = (int)status;

        var response = new
        {
            success = false,
            error = message
        };

        await context.Response.WriteAsync(JsonSerializer.Serialize(response));
    }
}