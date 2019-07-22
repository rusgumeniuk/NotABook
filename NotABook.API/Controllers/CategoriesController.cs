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
    public class CategoriesController : ControllerBase
    {
        private static IList<Category> list = new ObservableCollection<Category>() {
                new Category("First category"),
                new Category("Second category")
            };

        private IService _service;
        //public CategoriesController(IService service)
        //{
        //    _service = service;
        //}
        // GET: api/Categories
        [HttpGet]
        public IActionResult Get()
        {
            return Ok(list);
        }

        // GET: api/Categories/5
        [HttpGet("{id}", Name = "GetCategory")]
        public IActionResult Get(int id)
        {
            if (list.Count >= id)
                return Ok(list[id - 1]);
            else
                ModelState.AddModelError("Id", "Wrong ID");
            return NotFound(null);
        }

        // POST: api/Categories
        [HttpPost]
        public IActionResult Post([FromBody] Category value)
        {
            list.Add(value);
            return Ok(list[list.Count - 1]);
        }

        // PUT: api/Categories/5
        [HttpPut("{id}")]
        public IActionResult Put(int id, [FromBody] Category value)
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
