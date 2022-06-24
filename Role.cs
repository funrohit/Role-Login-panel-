using Microsoft.AspNetCore.Authentication;
using Microsoft.AspNetCore.Authentication.Cookies;
using Microsoft.AspNetCore.Mvc;
using Role.db;
using Role.Models;
using System.Diagnostics;
using System.Security.Claims;

namespace Role.Controllers
{
    public class HomeController : Controller
    {
        [HttpGet]
        public IActionResult Login()
        {
            return View();
        }


        [HttpPost]
        public IActionResult Login(admodel obj, normodel obj2)
        {


            loggContext database = new loggContext();


            var res = database.Admins.Where(a => a.Name == obj.Name).FirstOrDefault();



            if (res == null)
            {

                var res1 = database.Normals.Where(a => a.Name == obj.Name).FirstOrDefault();

                if (res1.Name == obj2.Name && res1.Pass == obj2.Pass)
                {

                    var claims = new[] { new Claim(ClaimTypes.Name, res1.Name) };

                    var identity = new ClaimsIdentity(claims, CookieAuthenticationDefaults.AuthenticationScheme);

                    var authProperties = new AuthenticationProperties
                    {
                        IsPersistent = true
                    };
                    HttpContext.SignInAsync(
                     CookieAuthenticationDefaults.AuthenticationScheme,
                     new ClaimsPrincipal(identity),
                   authProperties);

                    return RedirectToAction("B", "Home");

                }
            }
            else
            {
                if (res.Name == obj.Name && res.Pass == obj.Pass)
                {

                    var claims = new[] { new Claim(ClaimTypes.Name, res.Name) };

                    var identity = new ClaimsIdentity(claims, CookieAuthenticationDefaults.AuthenticationScheme);

                    var authProperties = new AuthenticationProperties
                    {
                        IsPersistent = true
                    };
                    HttpContext.SignInAsync(
                     CookieAuthenticationDefaults.AuthenticationScheme,
                     new ClaimsPrincipal(identity),
                   authProperties);

                    return RedirectToAction("A", "Home");
                }

                return View("Login");
            }
            return View("Login");
        }

            public IActionResult Admin()
            {
                return View();
            }

            public IActionResult A()
            {
                return View();
            }

            public IActionResult B()
            {
                return View();
            }

        
    }
}
