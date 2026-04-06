using System.Diagnostics;

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

        // Подготовка заголовка до вызова следующего middleware
        context.Response.OnStarting(() =>
        {
            stopwatch.Stop();
            if (!context.Response.HasStarted)
            {
                context.Response.Headers["X-Endpoint-Elapsed-Ms"] = stopwatch.ElapsedMilliseconds.ToString();
            }
            return Task.CompletedTask;
        });

        await _next(context);
    }
}