using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using NotABookLibraryStandart.DB;
using NotABookLibraryStandart.Models.BookElements;

namespace NotABook.API.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class BooksController : ControllerBase
    {
        private static IList<Book> list = new ObservableCollection<Book>();

        private IService _service;
        //public BooksController(IService service)
        //{
        //    _service = service;
        //}
        static BooksController()
        {
            list.Add(new Book("First book"));
            list.Add(new Book("Second book"));
        }
        // GET: api/Book
        [HttpGet]
        public IActionResult Get()
        {
            return Ok(list);
        }

        // GET: api/Book/5
        [HttpGet("{id}", Name = "GetBook")]
        public IActionResult Get(int id)
        {
            if (list.Count >= id)
                return Ok(list[id - 1]);
            else
                ModelState.AddModelError("Id", "Wrong ID");
            return NotFound(null);
        }

        // POST: api/Books
        [HttpPost]
        public IActionResult Post([FromBody] Book value)
        {
            list.Add(value);
            return Ok(list[list.Count - 1]);
        }

        // PUT: api/Books/5
        [HttpPut("{id}")]
        public IActionResult Put(int id, [FromBody] Book value)
        {
            if (list.Count >= id)
            {
                list[id - 1] = value;
                return Ok(list[id - 1]);
            }
            else
            {
                ModelState.AddModelError("Id", "Wrong ID");
                return NotFound(id);
            }
        }

        // DELETE: api/ApiWithActions/5
        [HttpDelete("{id}")]
        public IActionResult Delete(int id)
        {
            if (list.Count >= id)
            {
                list.RemoveAt(id - 1);
                return NoContent();
            }
            else
            {
                ModelState.AddModelError("Id", "Wrong ID");
                return NotFound(id);
            }
        }
    }
}
