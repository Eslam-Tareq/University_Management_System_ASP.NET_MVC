using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Filters;

namespace WebApplication1.Filters
{
    public class CheckLocation:ActionFilterAttribute
    {
        public override void OnActionExecuting(ActionExecutingContext context)
        {
            //var location = context.HttpContext.Request.Form["Location"].ToString()?.ToUpper();

            //if (string.IsNullOrEmpty(location) || (location != "USA" && location != "EG"))
            //{
            //    context.Result = new BadRequestObjectResult("Invalid location. Supported locations are: USA, EG");
            //}
            var location = context.HttpContext.Request.Form["Location"].ToString();

            if (location != "USA" && location != "EG")
            {
                context.ModelState.AddModelError("Location", "Location must be USA or EG");

                var controller = context.Controller as Controller;
                if (controller != null)
                {
                    context.Result = controller.View(context.ActionArguments.Values.FirstOrDefault());
                }
            }
        }
    }
}
