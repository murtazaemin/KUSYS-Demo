using KUSYS_Demo.Models;
using Microsoft.AspNetCore.Authentication;
using Microsoft.AspNetCore.Authentication.Cookies;
using Microsoft.AspNetCore.Mvc;
using System.Security.Claims;

namespace KUSYS_Demo.Controllers
{
    public class LoginController : Controller
    {
        Context _context;
        public LoginController(Context context)
        {
            _context = context;
        }
        [HttpGet]
        public IActionResult Login()
        {
            return View();
        }

        [HttpPost]
        public async Task<IActionResult> Login(User user)
        {

            var validUser = _context.Users.FirstOrDefault(x => x.UserName == user.UserName && x.Password == user.Password);

            if (validUser != null)
            {
                // claims verilerinin set edilmesi
                var claims = new List<Claim> {
                    new Claim(ClaimTypes.Name, validUser.UserName),
                    new Claim(ClaimTypes.Role, validUser.Role),
                    new Claim(ClaimTypes.NameIdentifier, validUser.StudentId.ToString())
                };
                // cookie settings düzenleme
                var claimsIdentity = new ClaimsIdentity(claims, CookieAuthenticationDefaults.AuthenticationScheme);
                var authProperties = new AuthenticationProperties
                {
                    IsPersistent = false, 
                };

                // Sign in işleminin gerçekleştirilmesi
                await HttpContext.SignInAsync(CookieAuthenticationDefaults.AuthenticationScheme, new ClaimsPrincipal(claimsIdentity), authProperties);

                return RedirectToAction("Index", "StudentsCourse");

            }
            else
            {
                return RedirectToAction("Index", "Login");
            }


        }

        [HttpGet]
        public async Task<IActionResult> Logout()
        {
            // Sign out işleminin gerçekleştirilmesi
            await HttpContext.SignOutAsync(CookieAuthenticationDefaults.AuthenticationScheme);
            return RedirectToAction("Login", "Login");
        }
    }
}
