using Microsoft.AspNetCore.Mvc;

namespace WebApplication1.Controllers
{
    public class StateManagementController : Controller
    {
        //session saved in the server
        public IActionResult SetSession(string userName, int password)
        {
            
            HttpContext.Session.SetString("userName", userName);
            HttpContext.Session.SetInt32("password", password);
            return Content("username and password set successfully");
        }

        public IActionResult GetSession()
        {
          string? usname=  HttpContext.Session.GetString("userName");
          int? password=  HttpContext.Session.GetInt32("password");

            return Content($"user name = {usname} , pass = {password}");
        }



        public IActionResult SetLanguageCookie(string lang)
        {
            CookieOptions cookieOptions = new();
            cookieOptions.Expires = DateTime.Now.AddDays(30);
            cookieOptions.Secure = true;
            cookieOptions.HttpOnly = true;

            HttpContext.Response.Cookies.Append("UserLanguage",lang,cookieOptions);
            return Content("Language set successfully");

        }

        public IActionResult GetCookie()
        {
            string? userlanguage = HttpContext.Request.Cookies["UserLanguage"];
            return Content($"user Language is {userlanguage}");

        }



    }
}
