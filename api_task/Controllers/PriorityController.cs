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
    public class PriorityController : ControllerBase
    {
        private readonly AppDbContext _context;
        public IConfiguration _configuration { get; }
        public PriorityController(AppDbContext context, IConfiguration configuration)
        {
            _context = context;
            _configuration = configuration;
        }
        [HttpGet]
        public async Task<ActionResult<IEnumerable<Priority>>> GetProducts()
        {
            return await _context.priority.ToListAsync();
        }
        [HttpGet("{id}")]
        public async Task<ActionResult<Priority>> GetProduct(int id)
        {
            var product = await _context.task.FindAsync(id);
            if (product == null)
            {
                return NotFound();
            }
            return product.Priority;
        }

        [HttpPut("{TaskId},{TagId}")]
        public async Task<IActionResult> UpdateProduct(int TaskID, int TagID)
        {
            var task = await _context.task.FindAsync(TaskID);
            var prio = await _context.priority.FindAsync(TagID);
            if (task == null || prio == null)
            {
                return NotFound();
            }

            task.Priority = prio;
            await _context.SaveChangesAsync();
            return NoContent();
        }
    }
}

