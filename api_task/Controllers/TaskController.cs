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
    public class TaskController : ControllerBase
    {
        private readonly AppDbContext _context;
        public IConfiguration _configuration { get; }
        public TaskController(AppDbContext context, IConfiguration configuration)
        {
            _context = context;
            _configuration = configuration;
        }
        
        [HttpGet]
        public async Task<ActionResult<IEnumerable<Quest>>> GetProducts()
        {
            return await _context.task.ToListAsync();
        }

        [HttpGet("{id}")]
        public async Task<ActionResult<Quest>> GetProduct(int id)
        {
            var product = await _context.task.FindAsync(id);
            if (product == null)
            {
                return NotFound();
            }
            return product;
        }
        [HttpGet("{id}")]
        public async Task<ActionResult<IEnumerable<Quest>>> GetProductByUser(int id)
        {
            var user = await _context.user.FindAsync(id);
            if (user == null)
            {
                return NotFound();
            }
            return user.Tasks;
        }

        [HttpPost("{Name},{desc},{userID},{categoryID},{stateID},{PriorityID}")]
        public async Task<IActionResult> CreateProduct(string nazwa, string desc, int userId, int categoryId,int stateId, int priorityId)
        {
            var user = _context.user.Find(userId);
            var cat = _context.category.Find(categoryId);
            var state = _context.state.Find(stateId);
            var prio = _context.priority.Find(priorityId);
            if(user == null||cat ==null||state==null || prio ==null)
            {
                return NotFound();
            }
            _context.task.Add(new Models.Quest { Name = nazwa,Description = desc, User = user,Category = cat, State = state, Priority = prio});
            _context.SaveChanges();
            return NoContent();
        }

        [HttpPut("{id},{Name},{desc},{categoryID},{stateID},{PriorityID}")]
        public async Task<IActionResult> UpdateProduct(int id,string nazwa, string desc,int categoryId, int stateId, int priorityId)
        {
            var task = _context.task.Find(id);
            var cat = _context.category.Find(categoryId);
            var state = _context.state.Find(stateId);
            var prio = _context.priority.Find(priorityId);
            if (cat == null || state == null || prio == null||task==null)
            {
                return NotFound();
            }
            task.Name = nazwa;
            task.Description = desc;
            task.Category = cat;
            task.State = state;
            task.Priority = prio;
            _context.SaveChangesAsync();
            return NoContent();
        }
        [HttpPut("{id},{UserID}")]
        public async Task<IActionResult> UpdateUser(int id, int userId)
        {
            var user = _context.user.Find(userId);
            if (user == null)
            {
                return NotFound();
            }
            _context.task.FirstOrDefault(x => x.Id == id).User = user;
            _context.SaveChangesAsync();
            return NoContent();
        }
        [HttpPut("{id}")]
        public async Task<IActionResult> DeleteUser(int id)
        {
            var task = _context.task.Find(id);
            if (task == null)
            {
                return NotFound();
            }
            _ = task.User == null;
            _context.SaveChangesAsync();
            return NoContent();
        }

        [HttpDelete("{id}")]
        public async Task<IActionResult> DeleteProduct(int id)
        {
            var product = await _context.task.FindAsync(id);
            if (product == null)
            {
                return NotFound();
            }
            _context.task.Remove(product);
            await _context.SaveChangesAsync();
            return NoContent();
        }
    }
}
