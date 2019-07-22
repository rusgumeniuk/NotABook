using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using NotABookLibraryStandart.DB;
using NotABookLibraryStandart.Models.Roles;

namespace NotABook.API.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class UsersController : ControllerBase
    {
        private static IList<User> list = new ObservableCollection<User>() { new User("First", "f@gmail.com", "users"),
                new User("Second", "f@gmail.com", "administators") };

        private IService _service;
        //public UsersController(IService service)
        //{
        //    _service = service;
        //}
        // GET: api/Users
        [HttpGet]
        public IActionResult Get()
        {
            return Ok(list);
        }

        // GET: api/Users/5
        [HttpGet("{id}", Name = "GetUser")]
        public IActionResult Get(int id)
        {
            if (list.Count >= id)
                return Ok(list[id - 1]);
            else
                ModelState.AddModelError("Id", "Wrong ID");
            return NotFound(null);
        }

        // POST: api/Users
        [HttpPost]
        public IActionResult Post([FromBody] User value)
        {
            list.Add(value);
            return Ok(list[list.Count - 1]);
        }

        // PUT: api/Users/5
        [HttpPut("{id}")]
        public IActionResult Put(int id, [FromBody] User value)
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
