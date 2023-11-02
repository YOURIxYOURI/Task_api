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
    public class StateController : ControllerBase
    {
        private readonly AppDbContext _context;
        public IConfiguration _configuration { get; }
        public StateController(AppDbContext context, IConfiguration configuration)
        {
            _context = context;
            _configuration = configuration;
        }
        [HttpGet]
        public async Task<ActionResult<IEnumerable<State>>> GetProducts()
        {
            return await _context.state.ToListAsync();
        }
        [HttpGet("{id}")]
        public async Task<ActionResult<State>> GetProduct(int id)
        {
            var product = await _context.task.FindAsync(id);
            if (product == null)
            {
                return NotFound();
            }
            return product.State;
        }

        [HttpPut("{TaskId},{TagId}")]
        public async Task<IActionResult> UpdateProduct(int TaskID, int TagID)
        {
            var task = await _context.task.FindAsync(TaskID);
            var state = await _context.state.FindAsync(TagID);
            if (task == null || state == null)
            {
                return NotFound();
            }

            task.State = state;
            await _context.SaveChangesAsync();
            return NoContent();
        }
    }
}

