using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Authentication.Cookies;
using BookStore.Models;
using System.Security.Claims;
using Microsoft.AspNetCore.Authentication;

namespace BookStore.Controllers
{
    public class AccessController : Controller
    {
        public IActionResult Login()

        {
            ClaimsPrincipal claimsuser = HttpContext.User;
            if (claimsuser.Identity.IsAuthenticated)
            { 
                return RedirectToAction("GetAll","Book"); ;
            }
            return View();
        }
        [HttpPost]
        public async Task<IActionResult> Login(VMLogin modelLogin)
        {
            if (modelLogin.Email == "admin@admin.com" && modelLogin.Password == "admin")
            {
                List<Claim> claims = new List<Claim>() { new Claim(ClaimTypes.NameIdentifier,modelLogin.Email),
                new Claim("OtherProperties","Example")
                };
                ClaimsIdentity claimsIdentity = new ClaimsIdentity(claims,CookieAuthenticationDefaults.AuthenticationScheme);

                AuthenticationProperties properties = new AuthenticationProperties() { 
                 AllowRefresh= true,
                 IsPersistent=modelLogin.KeepLoggedIn,

                };
                await HttpContext.SignInAsync(CookieAuthenticationDefaults.AuthenticationScheme,new ClaimsPrincipal(claimsIdentity), properties);
                return RedirectToAction("GetAll","Book");
            }
            ViewData["vmsg"] = "User Not Foudn";
            return View();
        }
    }
}
