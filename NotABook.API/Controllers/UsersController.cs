using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using NotABookLibraryStandart.Models.Roles;

namespace NotABook.API.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class UsersController : ControllerBase
    {
        // GET: api/Users
        [HttpGet]
        public IEnumerable<User> Get()
        {
            return new User[] 
            {
                new User("First", "f@gmail.com", "users"),
                new User("Second", "f@gmail.com", "administators")
            };
        }

        // GET: api/Users/5
        [HttpGet("{id}", Name = "GetUser")]
        public User Get(int id)
        {
            return new User();
        }

        // POST: api/Users
        [HttpPost]
        public void Post([FromBody] User value)
        {
        }

        // PUT: api/Users/5
        [HttpPut("{id}")]
        public void Put(int id, [FromBody] User value)
        {
        }

        // DELETE: api/ApiWithActions/5
        [HttpDelete("{id}")]
        public void Delete(int id)
        {
        }
    }
}
