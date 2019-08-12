using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using NotABook.WebAppCore.ViewModels;
using NotABookLibraryStandart.DB;

namespace NotABook.WebAppCore.Controllers
{
    public class NoteController : Controller
    {
        private IService service;
        public NoteController(IService _service)
        {
            service = _service;
        }
        public IActionResult Index()
        {
            var userBooks = service.FindBooks(service.GetUser(User.Identity.Name));
            return View(new MainPageViewModel(userBooks[0], userBooks, null));
        }

        public IActionResult NotePartial()
        {
            return PartialView();
        }

        internal IActionResult GetImage(byte[] bytes)
        {
            return File(bytes, "img/jpg");
        }
    }
}