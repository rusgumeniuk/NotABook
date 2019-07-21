using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using NotABookLibraryStandart.Models.BookElements.Contents;

namespace NotABook.API.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class ContentsController : ControllerBase
    {
        // GET: api/Contents
        [HttpGet]
        public IEnumerable<Content> Get()
        {
            return new Content[]
            {
                new TextContent(),
                new PhotoContent()
            };
        }

        // GET: api/Contents/5
        [HttpGet("{id}", Name = "GetContent")]
        public Content Get(int id)
        {
            return new TextContent();
        }

        // POST: api/Contents
        [HttpPost]
        public void Post([FromBody] Content value)
        {
        }

        // PUT: api/Contents/5
        [HttpPut("{id}")]
        public void Put(int id, [FromBody] Content value)
        {
        }

        // DELETE: api/ApiWithActions/5
        [HttpDelete("{id}")]
        public void Delete(int id)
        {
        }
    }
}
