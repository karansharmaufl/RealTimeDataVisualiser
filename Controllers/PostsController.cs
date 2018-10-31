using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.EntityFrameworkCore;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using System.Net;

namespace DataVisualiser.Controllers
{
    [Route("api/Posts")]
    [Produces("application/json")]
    public class PostsController : ControllerBase
    {

        private readonly Models.SelfHostingTutorialContext _context;
        public PostsController(Models.SelfHostingTutorialContext context)
        {
            _context = context;
        }

        private bool PostExists(int id)
        {
            return _context.TPost.Any(c => c.PkPostId == id);
        }

        [HttpGet]
        public IActionResult GetPost()  
        {
            // Understand this asap
            var results = new ObjectResult(_context.TPost)
            {
                StatusCode = (int)HttpStatusCode.OK
            };

            Request.HttpContext.Response.Headers.Add("X-TOTAL-COUNT", _context.TPost.Count().ToString());
            return results;
        }

        [HttpGet("{id}", Name = "GetPost")]
        public async Task<IActionResult> GetPostAsync([FromRoute] int id)
        {
            if (PostExists(id))
            {
                var post = await _context.TPost.SingleOrDefaultAsync(m => m.PkPostId == id);
                return Ok(post);
            }
            else
            {
                return NotFound();
            }
            
        }

        [HttpPost]
        public async Task<IActionResult> PostPost([FromBody]  Models.TPost post)
        {
            // Validate here
            if (!ModelState.IsValid)   // Set during data binding. Checks data binding state
            {
                return BadRequest(ModelState);
            }

            _context.TPost.Add(post);
            await _context.SaveChangesAsync();
            return CreatedAtAction("getPost", new { id = post.PkPostId });
        }

        [HttpPut("{id}")]
        public async Task<IActionResult> PutPostAsync([FromRoute] int id, [FromBody] Models.TPost post)
        {
            _context.Entry(post).State = Microsoft.EntityFrameworkCore.EntityState.Modified;
            await _context.SaveChangesAsync();
            return Ok(post);
        }

        [HttpDelete("{id}")]
        public async Task<IActionResult> DeletePost([FromRoute] int id)
        {
            var post = await _context.TPost.SingleOrDefaultAsync(m => m.PkPostId == id);
            _context.TPost.Remove(post);
            await _context.SaveChangesAsync();
            return Ok(post);
        }
    }
}