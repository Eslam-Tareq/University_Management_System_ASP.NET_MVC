using Microsoft.AspNetCore.Mvc.Filters;

namespace WebApplication1.Filters
{
    public class AddInfoToResponse : IResultFilter
    {
        public void OnResultExecuted(ResultExecutedContext context)
        {
            

        }

        public void OnResultExecuting(ResultExecutingContext context)
        {
            context.HttpContext.Response.Headers["App-Name"] = "MyFirst MvC App";
            context.HttpContext.Response.Headers["Developed-By"] = "ESLAM TAREK";
        }
    }
}
