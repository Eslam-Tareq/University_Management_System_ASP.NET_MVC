namespace WebApplication1.Middlewares
{
    public class TestMiddleware
    {
        private RequestDelegate next;
        public TestMiddleware(RequestDelegate _next)
        {
            next = _next;
        }
        public async Task Invoke(HttpContext httpContext)
        {
            Console.WriteLine("start middleware 1 ");
            await next(httpContext);
            Console.WriteLine("end middleware 1");
        }
    }
    public static class TestMiddleWareExtension {

        static public IApplicationBuilder UseTestMiddleware(this IApplicationBuilder application)
        {
            return application.UseMiddleware<TestMiddleware>();
        }
        }
}
