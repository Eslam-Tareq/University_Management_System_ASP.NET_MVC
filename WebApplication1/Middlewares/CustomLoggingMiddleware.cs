using System.Diagnostics;

namespace WebApplication1.Middlewares
{

    public class RequestLoggingMiddleware
    {
        private readonly RequestDelegate _next;
        private readonly ILogger<RequestLoggingMiddleware> _logger;

        public RequestLoggingMiddleware(RequestDelegate next, ILogger<RequestLoggingMiddleware> logger)
        {
            _next = next;
            _logger = logger;
        }

        public async Task InvokeAsync(HttpContext context)
        {
            var startTime = Stopwatch.StartNew();
            var request = context.Request;

            _logger.LogInformation("Incoming Request: {Method} {Path}", request.Method, request.Path);

            await _next(context);

            startTime.Stop();
            var statusCode = context.Response.StatusCode;

            _logger.LogInformation("Finished Request: {Method} {Path} responded {Status} in {Elapsed}ms",
                request.Method, request.Path, statusCode, startTime.ElapsedMilliseconds);
        }
    }
    public static class CustomLoggerExtension
    {
        public static IApplicationBuilder UseCustomLoggingMiddleware(this IApplicationBuilder application)
        {
            return application.UseMiddleware<RequestLoggingMiddleware>();
        }
    }
}
