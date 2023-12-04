using api_task;
using api_task.Models;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Configuration;
using Pomelo.EntityFrameworkCore.MySql;
using System.Threading.Tasks;

namespace api_task.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class TagController : ControllerBase
    {
        private readonly AppDbContext _context;
        public IConfiguration _configuration { get; }
        public TagController(AppDbContext context, IConfiguration configuration)
        {
            _context = context;
            _configuration = configuration;
        }

        [HttpGet("{id}")]
        public async Task<ActionResult<IEnumerable<Comment>>> GetComments(int id)
        {
            var product = await _context.task.FindAsync(id);
            if (product == null)
            {
                return NotFound();
            }
            return product.Comments;
        }
        [HttpPut("{id},{desc}")]
        public async Task<IActionResult> PutComment(int id, string desc)
        {
            var com = await _context.comment.FindAsync(id);
            if (com == null)
            {
                return NotFound();
            }
            com.Description = desc;
            await _context.SaveChangesAsync();
            return NoContent();
        }

        [HttpPost("{nazwa},{taskID}")]
        public void CreateComment(string nazwa,int taskID)
        {
            var task = _context.task.Find(taskID);
            var com = new Comment { Description = nazwa };
            _context.comment.Add(com);
            task.Comments.Add(com);
            _context.SaveChangesAsync();
        }

        [HttpDelete("{TaskId},{TagId}")]
        public async Task<IActionResult> DeleteComment(int TaskID, int TagID)
        {
            var task = await _context.task.FindAsync(TaskID);
            var com = await _context.comment.FindAsync(TagID);
            if (task == null|| com ==null)
            {
                return NotFound();
            }

            task.Comments.Remove(com);
            await _context.SaveChangesAsync();
            return NoContent();
        }
    }
}

