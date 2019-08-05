using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using NotABookLibraryStandart.Models.BookElements;

namespace NotABook.WebAppCore.Controllers
{
    public class HomeController : Controller
    {
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
            return View();
        }

        public IActionResult SignUp()
        {
            return View();
        }
    }
}