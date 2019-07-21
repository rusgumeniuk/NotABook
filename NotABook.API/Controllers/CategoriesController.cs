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
    public class CategoriesController : ControllerBase
    {
        // GET: api/Categories
        [HttpGet]
        public IEnumerable<Category> Get()
        {
            return new Category[] 
            {
                new Category("First category"),
                new Category("Second category")
            };
        }

        // GET: api/Categories/5
        [HttpGet("{id}", Name = "GetCategory")]
        public Category Get(int id)
        {
            return new Category($"{id}");
        }

        // POST: api/Categories
        [HttpPost]
        public void Post([FromBody] Category value)
        {
        }

        // PUT: api/Categories/5
        [HttpPut("{id}")]
        public void Put(int id, [FromBody] Category value)
        {
        }

        // DELETE: api/ApiWithActions/5
        [HttpDelete("{id}")]
        public void Delete(int id)
        {
        }
    }
}
