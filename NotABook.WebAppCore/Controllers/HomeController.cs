using Microsoft.AspNetCore.Authentication;
using Microsoft.AspNetCore.Authentication.Cookies;
using Microsoft.AspNetCore.Mvc;
using NotABook.WebAppCore.ViewModels;
using NotABookLibraryStandart.DB;
using NotABookLibraryStandart.Models.BookElements;
using NotABookLibraryStandart.Models.Roles;
using System;
using System.Collections.Generic;
using System.Security.Claims;
using System.Threading.Tasks;

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

        public IActionResult Login()
        {
            return View(new LoginViewModel());
        }
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Login(LoginViewModel loginViewModel)
        {
            if (ModelState.IsValid)
            {
                User user = null;
                try
                {
                    user = service.GetUser(loginViewModel.Username, loginViewModel.Password);
                }
                catch (UnauthorizedAccessException)
                {
                    ModelState.AddModelError("", "Wrong creaitals!\nPlease try again");
                }
                if (user != null)
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

        [HttpGet]
        public IActionResult Signup()
        {
            return View(new SignupViewModel());
        }
        [HttpPost]
        public IActionResult Signup(SignupViewModel viewModel)
        {
            if (ModelState.IsValid)
            {
                if (service.GetUser(viewModel.Username) != null)
                    ModelState.AddModelError("Username", "Oooops, we already have user with same username. Try another please.");
                else if (service.GetUserByEmail(viewModel.Email) != null)
                    ModelState.AddModelError("Email", "Oh, Seems like we already have user with same email. Recover password or try another email please");
                else
                {
                    service.AddUser(new User(viewModel.Username, viewModel.Email, NotABookLibraryStandart.Models.Roles.User.CalculateHash(viewModel.Password, viewModel.Username),"Users"));
                    return Redirect("Login");
                }
            }
            else
            {
                ModelState.AddModelError("", "Wrong info, try again!");
            }
            return View();
        }
        public IActionResult ForgotPassword()
        {
            return View();
        }
    }
}