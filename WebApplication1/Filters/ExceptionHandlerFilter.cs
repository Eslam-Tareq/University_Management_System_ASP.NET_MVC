using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Filters;
using Microsoft.AspNetCore.Mvc.ModelBinding;
using Microsoft.AspNetCore.Mvc.ViewFeatures;
using WebApplication1.Utils;

namespace WebApplication1.Filters
{
    public class ExceptionHandlerFilter : IExceptionFilter
    {
        void IExceptionFilter.OnException(ExceptionContext context)
        {
            // Initialize ViewResult
            var viewResult = new ViewResult
            {
                ViewName = "Error",
                // Initialize ViewData properly to avoid NullReferenceException
                ViewData = new ViewDataDictionary(new EmptyModelMetadataProvider(), context.ModelState)
            };

            var error = context.Exception;

            // Now you can safely assign values
            viewResult.ViewData["Message"] = error.Message;
            viewResult.ViewData["StackTrace"] = error.StackTrace;

            if (error is CustomException ce)
            {
                viewResult.ViewData["Status"] = ce.Status;
                viewResult.ViewData["StatusCode"] = ce.StatusCode;
            }
            else
            {
                viewResult.ViewData["Status"] = "Internal Server Error";
                viewResult.ViewData["StatusCode"] = 500;
            }

            context.Result = viewResult;
            context.ExceptionHandled = true; // Mark as handled so other filters don't run
        }
    }
}
