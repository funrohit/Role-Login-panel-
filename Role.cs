using Microsoft.AspNetCore.Authentication;
using Microsoft.AspNetCore.Authentication.Cookies;
using Microsoft.AspNetCore.Mvc;
using RollingAuth.db;
using RollingAuth.Models;
using System.Diagnostics;
using System.Security.Claims;

namespace RollingAuth.Controllers
{
    public class HomeController : Controller
    {
        [HttpGet]
        public IActionResult Login()
        {
            return View();
        }


        [HttpPost]
        public IActionResult Login(submitmodel obj)
        {


            RollingContext database = new RollingContext();


            var res = database.Admins.Where(a => a.Name == obj.Name).FirstOrDefault();

          
            if (res == null)
            {

                var res1 = database.Hrs.Where(a => a.Name == obj.Name).FirstOrDefault();

                if (res1 == null)
                {
                    ViewBag.data = "Plz Enter Correct password and UserName!";
                    return View("Login");
                }

                else if(res1.Name == obj.Name && res1.Pass == obj.Pass)
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

                    return RedirectToAction("HR", "Home");

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

                    return RedirectToAction("Admin", "Home");
                }

                return View("Login");
            }
            return View("Login");
        }

        public IActionResult Admin()
        {
            return View();
        }

        public IActionResult HR()
        {
            return View();
        }

    }
}


=======================================================================================================================================

More---------------------------------
    
    
    
    
    using Microsoft.AspNetCore.Mvc;
using Rolling1Table.db;
using Rolling1Table.Models;
using System.Diagnostics;

namespace Rolling1Table.Controllers
{
    public class HomeController : Controller
    {
        [HttpGet]
        public IActionResult Login()
        {
            return View();
        }



        [HttpPost]
        public IActionResult Login(mymodel obj)
        {
            RollitContext database = new RollitContext();

            if (obj.Roll == "HR")
            {
                var res = database.Alldata.Where(n=>n.Name == obj.Name).FirstOrDefault();

                if (res == null)
                {
                    ViewBag.inv = "Make Sure Your UserName,Password And Role Should Be Correct !";
                }
                else if(res.Roll == obj.Roll  && res.Pass==obj.Pass)
                {

                    return View("V","Home");
                }

                ViewBag.inv = "Make Sure Your UserName,Password And Role Should Be Correct !";
                return View("Login");

            }

            if(obj.Roll == "ADMIN")
            {
                var res = database.Alldata.Where(n => n.Name == obj.Name).FirstOrDefault();

                if (res == null)
                {
                    ViewBag.inv = "Make Sure Your UserName,Password And Role Should Be Correct !";
                }
                else if (res.Roll == obj.Roll && res.Pass == obj.Pass)
                {
                    return View("AD", "Home");
                }

                ViewBag.inv = "Make Sure Your UserName,Password And Role Should Be Correct !";
                return View("Login");
            }

            if (obj.Roll == "MANAGER")
            {
                var res = database.Alldata.Where(n => n.Name == obj.Name).FirstOrDefault();

                if (res == null)
                {
                    ViewBag.inv = "Make Sure Your UserName,Password And Role Should Be Correct !";
                }
                else if (res.Roll == obj.Roll && res.Pass==obj.Pass)
                {
                    return View("MAN", "Home");
                }

                ViewBag.inv = "Make Sure Your UserName,Password And Role Should Be Correct !";
                return View("Login");
            }          

            return View();
        }

        public IActionResult V()
        {
            return View();
        }

        public IActionResult AD()
        {
            return View();
        }

        public IActionResult MAN()
        {
            return View();
        }
    }
}
