using System.Net;
using System.Text.Json;

namespace Api.Middleware;

public class ErrorHandlerMiddleware
{
    private readonly RequestDelegate _next;

    public ErrorHandlerMiddleware(RequestDelegate next)
    {
        _next = next;
    }

    public async Task Invoke(HttpContext context)
    {
        try
        {
            await _next(context);
        }
        catch (ArgumentException ex)
        {
            await Handle(context, ex.Message, HttpStatusCode.BadRequest);
        }
        catch (Exception)
        {
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