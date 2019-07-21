using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using NotABookLibraryStandart.Models.BookElements;

namespace NotABook.API.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class BooksController : ControllerBase
    {
        // GET: api/Book
        [HttpGet]
        public IEnumerable<Book> Get()
        {
            return new List<Book>()
            {
                new Book("First book"),
                new Book("Second book")
            };
        }

        // GET: api/Book/5
        [HttpGet("{id}", Name = "GetBook")]
        public Book Get(int id)
        {
            return new Book($"{id}");
        }

        // POST: api/Books
        [HttpPost]
        public void Post([FromBody] Book value)
        {
        }

        // PUT: api/Books/5
        [HttpPut("{id}")]
        public void Put(int id, [FromBody] Book value)
        {
        }

        // DELETE: api/ApiWithActions/5
        [HttpDelete("{id}")]
        public void Delete(int id)
        {
        }
    }
}
