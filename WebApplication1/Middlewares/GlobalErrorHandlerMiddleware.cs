using Microsoft.AspNetCore.Http.HttpResults;
using WebApplication1.Dtos;
using WebApplication1.Utils;

namespace WebApplication1.Middlewares
{
    public class GlobalErrorHandlerMiddleware
    {
        private RequestDelegate next;
        public GlobalErrorHandlerMiddleware(RequestDelegate _next)
        {
            next = _next;
        }
        private async Task HandleException(HttpContext context, int statusCode, string message, string? stackTrace)
        {
            context.Response.StatusCode = statusCode;

            await context.Response.WriteAsJsonAsync(new ErrorResponse
            {
                Status = statusCode >= 400 && statusCode < 500 ? "fail" : "error",
                StatusCode = statusCode,
                Message = message,
                StackTrace = stackTrace
            });
        }
        public async Task Invoke (HttpContext httpContext)
        {
            try
            {
                await next(httpContext);
            }

            catch (CustomException e)
            {
                await HandleException(httpContext, e.StatusCode, e.Message, e.StackTrace);
            }
            catch (Exception e)
            {
                await HandleException(httpContext, 500, e.Message, e.StackTrace);
            }
        }
    }
    public static class GlobalErrorHandlerMiddlewareExtention
    {
        public static IApplicationBuilder UseGlobalErrorHandler(this IApplicationBuilder application)
        {
           
            return application.UseMiddleware<GlobalErrorHandlerMiddleware>();
        }
    }
}
