using System.Text;

namespace lab8.Middleware;

public class BlockPathMiddleware
{
    private readonly RequestDelegate _next;

    public BlockPathMiddleware(RequestDelegate next)
    {
        _next = next;
    }

    public async Task InvokeAsync(HttpContext context)
    {
        if (context.Request.Path.StartsWithSegments("/blocked"))
        {
            context.Response.StatusCode = 403;
            context.Response.ContentType = "text/plain; charset=utf-8";

            await context.Response.WriteAsync("Доступ запрещён", Encoding.UTF8);
            return;
        }

        await _next(context);
    }
}