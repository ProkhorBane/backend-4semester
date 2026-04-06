using System.Diagnostics;

namespace MiddlewareDemo.Middleware;

public class EndpointTimingMiddleware
{
    private readonly RequestDelegate _next;

    public EndpointTimingMiddleware(RequestDelegate next)
    {
        _next = next;
    }

    public async Task InvokeAsync(HttpContext context)
    {
        var stopwatch = Stopwatch.StartNew();

        await _next(context);

        stopwatch.Stop();

        context.Response.Headers["X-Endpoint-Elapsed-Ms"] =
            stopwatch.ElapsedMilliseconds.ToString();
    }
}