using System;
using System.Collections.Generic;
using System.Security.Claims;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Authentication;
using Microsoft.AspNetCore.Authentication.Cookies;
using Microsoft.AspNetCore.Mvc;
using NotABookLibraryStandart.DB;
using NotABookLibraryStandart.Models.BookElements;
using NotABookLibraryStandart.Models.Roles;

namespace NotABook.WebAppCore.Controllers
{
    public class HomeController : Controller
    {
        private readonly IService service;
        public HomeController(IService _service)
        {                      
            service = _service;
        }
        public IActionResult Index()
        {
            return View(new Book("First web book"));
        }

        public IActionResult About()
        {
            return View();
        }

        public IActionResult LogIn()
        {
            return View(new User());
        }
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> LogIn(string username, string password)
        {
            if (ModelState.IsValid)
            {
                User user = null;
#if DEBUG
                user = new User("Ruslan", "rus.gumeniuk@gmail.com", "TySiVs1JHrD5R7etJorugFp5HcDMknAbZi1UK0KyPzw=", "Administrators");
#else
                try
                {
                    user = service.GetUser(username, password);
                }
                catch(UnauthorizedAccessException)
                {
                    ModelState.AddModelError("", "Wrong creaitals!\nPlease try again");
                }                
#endif
                if(user != null)
                {
                    await Authenticate(user.Username);
                    return RedirectToAction("Index");
                }                
            }

            return View();
        }

        private async Task Authenticate(string userName)
        {            
            var claims = new List<Claim>
            {
                new Claim(ClaimsIdentity.DefaultNameClaimType, userName)
            };            
            ClaimsIdentity id = new ClaimsIdentity(claims, "ApplicationCookie", ClaimsIdentity.DefaultNameClaimType, ClaimsIdentity.DefaultRoleClaimType);            
            await HttpContext.SignInAsync(CookieAuthenticationDefaults.AuthenticationScheme, new ClaimsPrincipal(id));
        }

        public async Task<IActionResult> Logout()
        {
            await HttpContext.SignOutAsync(CookieAuthenticationDefaults.AuthenticationScheme);
            return RedirectToAction("Login");
        }

        public IActionResult SignUp()
        {
            return View();
        }
        public IActionResult ForgotPassword()
        {
            return View();
        }
    }
}