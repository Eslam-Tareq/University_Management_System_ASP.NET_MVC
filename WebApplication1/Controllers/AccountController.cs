using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;
using System.Security.Claims;
using WebApplication1.Models;
using WebApplication1.ViewModels.Auth;

namespace WebApplication1.Controllers
{
    public class AccountController : Controller
    {
        private readonly UserManager<ApplicationUser> userManager;
        private readonly SignInManager<ApplicationUser> signInManager;


        public AccountController(UserManager<ApplicationUser> userManager, SignInManager<ApplicationUser> signInManager)
        {
            this.userManager = userManager;
            this.signInManager = signInManager;

        }

        // GET
        public IActionResult Register()
        {
            return View();
        }

        // POST
        [HttpPost]
        public async Task<IActionResult> Register(RegisterViewModel model)
        {
            if (ModelState.IsValid)
            {
                ApplicationUser user = new ApplicationUser()
                {
                    UserName = model.Email,
                    Email = model.Email,
                    FullName = model.FullName
                };
                var result = await userManager.CreateAsync(user, model.Password);

                if (result.Succeeded)
                {
                    return RedirectToAction("Login");
                }

                foreach (var error in result.Errors)
                {
                    ModelState.AddModelError("Password", error.Description);
                }
            }

            return View("Register",model);
        }
        public IActionResult Login()
        {
            return View();
        }

        // POST
        [HttpPost]
        public async Task<IActionResult> Login(LoginViewModel model)
        {
            if (ModelState.IsValid)
            {
                var result = await signInManager.PasswordSignInAsync(
                    model.Email,       
                    model.Password,
                    model.RememberMe,
                    false               
                );

                if (result.Succeeded)
                {
                    return RedirectToAction("Index", "Home");
                }

                ModelState.AddModelError("Email", "Invalid Email or Password");
                ModelState.AddModelError("Password", "Invalid Email or Password");
            }

            return View(model);
        }
        public IActionResult AccessDenied()
        {
            return View();
        }
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Logout()
        {
            await signInManager.SignOutAsync();
            return RedirectToAction("Login", "Account");
        }


        public IActionResult GoogleLogin()
        {
            var redirectUrl = Url.Action("GoogleResponse");
            var properties = signInManager.ConfigureExternalAuthenticationProperties("Google", redirectUrl);
            return Challenge(properties, "Google");
        }

        public async Task<IActionResult> GoogleResponse()
        {
            var info = await signInManager.GetExternalLoginInfoAsync();

            if (info == null)
                return RedirectToAction("Login");

            var result = await signInManager.ExternalLoginSignInAsync(
                info.LoginProvider,
                info.ProviderKey,
                isPersistent: false
            );

            if (result.Succeeded)
            {
                return RedirectToAction("Index", "Home");
            }
            else
            {
                var email = info.Principal.FindFirstValue(System.Security.Claims.ClaimTypes.Email);
                var fullname = info.Principal.FindFirstValue(System.Security.Claims.ClaimTypes.Name);

                var user = new ApplicationUser
                {
                    UserName = email,
                    Email = email,FullName=fullname
                };

                await userManager.CreateAsync(user);

                await userManager.AddLoginAsync(user, info);

                await signInManager.SignInAsync(user, false);

                return RedirectToAction("Index", "Home");
            }
        }
    }
}
