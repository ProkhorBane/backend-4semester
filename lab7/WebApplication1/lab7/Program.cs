using lab7.Middleware;
using MiddlewareDemo.Middleware;

var builder = WebApplication.CreateBuilder(args);

var app = builder.Build();

app.UseMiddleware<BlockPathMiddleware>();
app.UseMiddleware<RequestTraceMiddleware>();
app.UseMiddleware<EndpointTimingMiddleware>();


app.MapGet("/ping", () => "pong");

app.MapGet("/trace", (HttpContext context) =>
{
    return context.Items["TraceId"]?.ToString();
});

app.MapGet("/error", () =>
{
    throw new Exception("Test exception");
});

app.Run();