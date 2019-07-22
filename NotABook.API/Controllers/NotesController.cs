using Microsoft.AspNetCore.Mvc;
using NotABookLibraryStandart.DB;
using NotABookLibraryStandart.Models.BookElements;
using System.Collections.Generic;
using System.Collections.ObjectModel;

namespace NotABook.API.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class NotesController : ControllerBase
    {
        private static IList<Note> list = new ObservableCollection<Note>()
        {
            new Note("First note"),
                new Note("second note")
        };

        private IService _service;
        //public NotesController(IService service)
        //{
        //    _service = service;
        //}
        // GET: api/Notes
        [HttpGet]
        public IActionResult Get()
        {
            return Ok(list);
        }

        // GET: api/Notes/5
        [HttpGet("{id}", Name = "GetNote")]
        public IActionResult Get(int id)
        {
            if (list.Count >= id)
                return Ok(list[id - 1]);
            else
                ModelState.AddModelError("Id", "Wrong ID");
            return NotFound(null);
        }

        // POST: api/Notes
        [HttpPost]
        public IActionResult Post([FromBody] Note value)
        {
            list.Add(value);
            return Ok(list[list.Count - 1]);
        }

        // PUT: api/Notes/5
        [HttpPut("{id}")]
        public IActionResult Put(int id, [FromBody] Note value)
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
