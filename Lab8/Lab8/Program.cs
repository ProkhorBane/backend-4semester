using lab8.Middleware;

var builder = WebApplication.CreateBuilder(args);
var app = builder.Build();

app.UseExceptionHandler("/error-handler");

app.UseMiddleware<BlockPathMiddleware>();
app.UseMiddleware<RequestTraceMiddleware>();
app.UseMiddleware<EndpointTimingMiddleware>();

app.UseDefaultFiles();
app.UseStaticFiles();

app.MapGet("/error-handler", () => Results.Problem("Произошла ошибка"));

app.MapGet("/ping", () => "pong");

app.MapGet("/trace", (HttpContext context) =>
{
    var traceId = context.Items["TraceId"]?.ToString();
    return $"TraceId: {traceId}";
});

app.MapGet("/error", () =>
{
    throw new Exception("Тестовая ошибка");
});

app.MapGet("/time", () =>
{
    return $"Текущее время: {DateTime.Now}";
});

app.Run();