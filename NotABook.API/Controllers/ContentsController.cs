using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using NotABookLibraryStandart.DB;
using NotABookLibraryStandart.Models.BookElements.Contents;

namespace NotABook.API.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class ContentsController : ControllerBase
    {
        private static IList<Content> list = new ObservableCollection<Content>()
        {
            new TextContent(),
                new PhotoContent()
        };

        private IService _service;
        //public ContentsController(IService service)
        //{
        //    _service = service;
        //}
        // GET: api/Contents
        [HttpGet]
        public IActionResult Get()
        {
            return Ok(list);
        }

        // GET: api/Contents/5
        [HttpGet("{id}", Name = "GetContent")]
        public IActionResult Get(int id)
        {
            if (list.Count >= id)
                return Ok(list[id - 1]);
            else
                ModelState.AddModelError("Id", "Wrong ID");
            return NotFound(null);
        }

        // POST: api/Contents
        [HttpPost]
        public IActionResult Post([FromBody] Content value)
        {
            list.Add(value);
            return Ok(list[list.Count - 1]);
        }

        // PUT: api/Contents/5
        [HttpPut("{id}")]
        public IActionResult Put(int id, [FromBody] Content value)
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
