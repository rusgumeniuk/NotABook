using Microsoft.AspNetCore.Mvc;
using NotABookLibraryStandart.Models.BookElements;
using System.Collections.Generic;

namespace NotABook.API.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class NotesController : ControllerBase
    {
        // GET: api/Notes
        [HttpGet]
        public IEnumerable<Note> Get()
        {
            return new Note[]
            {
                new Note("First note"),
                new Note("second note")
            };
        }

        // GET: api/Notes/5
        [HttpGet("{id}", Name = "GetNote")]
        public Note Get(int id)
        {
            return new Note($"{id}");
        }

        // POST: api/Notes
        [HttpPost]
        public void Post([FromBody] Note value)
        {
        }

        // PUT: api/Notes/5
        [HttpPut("{id}")]
        public void Put(int id, [FromBody] Note value)
        {
        }

        // DELETE: api/ApiWithActions/5
        [HttpDelete("{id}")]
        public void Delete(int id)
        {
        }
    }
}
