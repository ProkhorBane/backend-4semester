using System.Diagnostics;

namespace lab8.Middleware;

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

        try
        {
            await _next(context);
        }
        finally
        {
            stopwatch.Stop();

            if (!context.Response.HasStarted)
            {
                context.Response.Headers["X-Endpoint-Elapsed-Ms"] =
                    stopwatch.ElapsedMilliseconds.ToString();
            }
        }
    }
}