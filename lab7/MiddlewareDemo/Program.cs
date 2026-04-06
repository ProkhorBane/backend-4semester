using MiddlewareDemo.Middleware;

var builder = WebApplication.CreateBuilder(args);

var app = builder.Build();


app.UseMiddleware<BlockPathMiddleware>();
app.UseMiddleware<RequestTraceMiddleware>();
app.UseMiddleware<EndpointTimingMiddleware>();


app.MapGet("/", () => "Hello World!");
app.MapGet("/ping", () => "pong");

app.MapGet("/trace", (HttpContext context) =>
{
    var traceId = context.Items.ContainsKey("TraceId") 
        ? context.Items["TraceId"]?.ToString() 
        : "no-trace";
    return $"TraceId: {traceId}";
});

app.MapGet("/error", () =>
{
    throw new Exception("Test exception");
});

app.Run();