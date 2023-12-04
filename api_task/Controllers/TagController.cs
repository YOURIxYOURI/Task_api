using api_task;
using api_task.Models;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Configuration;
using Pomelo.EntityFrameworkCore.MySql;

namespace api_task.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class CommentController : ControllerBase
    {
        private readonly AppDbContext _context;
        public IConfiguration _configuration { get; }
        public CommentController(AppDbContext context, IConfiguration configuration)
        {
            _context = context;
            _configuration = configuration;
        }

        [HttpGet("{id}")]
        public async Task<ActionResult<IEnumerable<Tag>>> GetTag(int id)
        {
            var product = await _context.task.FindAsync(id);
            if (product == null)
            {
                return NotFound();
            }
            return product.Tags;
        }

        [HttpPost("{nazwa}")]
        public void CreateTag(string nazwa)
        {
            _context.tag.Add(new Tag { Name = nazwa});
            _context.SaveChangesAsync();
        }

        [HttpPut("{TaskId},{TagId}")]
        public async Task<IActionResult> UpdateTag(int TaskID, int TagID)
        {
            var task = await _context.task.FindAsync(TaskID);
            var taq = await _context.tag.FindAsync(TagID);
            if (task == null || taq == null)
            {
                return NotFound();
            }

            task.Tags.Add(taq);
            await _context.SaveChangesAsync();
            return NoContent();
        }


        [HttpDelete("{TaskId},{TagId}")]
        public async Task<IActionResult> DeleteTag(int TaskID, int TagID)
        {
            var task = await _context.task.FindAsync(TaskID);
            var taq = await _context.tag.FindAsync(TagID);
            if (task == null|| taq ==null)
            {
                return NotFound();
            }

            task.Tags.Remove(taq);
            await _context.SaveChangesAsync();
            return NoContent();
        }
    }
}

