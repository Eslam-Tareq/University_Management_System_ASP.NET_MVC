namespace WebApplication1.Middlewares
{
    public class MyMiddleware
    {
        private RequestDelegate _next;
        public MyMiddleware(RequestDelegate next)
        {
            _next = next;
        }

        public async Task Invoke(HttpContext httpContext)
        {
            Console.WriteLine("from my middleware");

            throw new NotImplementedException();
            await _next(httpContext);

        }

    }
}
