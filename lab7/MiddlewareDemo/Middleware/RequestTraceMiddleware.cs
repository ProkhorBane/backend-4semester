using System;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Http;

namespace MiddlewareDemo.Middleware
{
    public class RequestTraceMiddleware
    {
        private readonly RequestDelegate _next;

        public RequestTraceMiddleware(RequestDelegate next)
        {
            _next = next;
        }

        public async Task InvokeAsync(HttpContext context)
        {
            var traceId = Guid.NewGuid().ToString();
            context.Items["TraceId"] = traceId;

            context.Response.OnStarting(() =>
            {
                context.Response.Headers["X-Trace-Id"] = traceId;
                return Task.CompletedTask;
            });

            await _next(context);
        }
    }
}