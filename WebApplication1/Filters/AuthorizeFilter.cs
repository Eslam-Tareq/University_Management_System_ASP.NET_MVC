using Microsoft.AspNetCore.Mvc.Filters;
using WebApplication1.Utils;

namespace WebApplication1.Filters
{
    public class AuthorizeFilterAttribute : Attribute,IAuthorizationFilter
    {
        public void OnAuthorization(AuthorizationFilterContext context)
        {
            var AppRole = context.HttpContext.Request.Headers["App-Role"].ToString();
            if(AppRole != "Student")
            {
                throw new CustomException("Only Student Role is allowed", StatusCodes.Status403Forbidden);
            }

        }
    }
}
