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
    public class UserController : ControllerBase
    {
        private readonly AppDbContext _context;
        public IConfiguration _configuration { get; }
        public UserController(AppDbContext context, IConfiguration configuration)
        {
            _context = context;
            _configuration = configuration;
        }
        
        [HttpGet]
        public async Task<ActionResult<IEnumerable<User>>> GetProducts()
        {
            return await _context.user.ToListAsync();
        }

        [HttpGet("{id}")]
        public async Task<ActionResult<User>> GetProduct(int id)
        {
            var product = await _context.user.FindAsync(id);
            if (product == null)
            {
                return NotFound();
            }
            return product;
        }

        [HttpPost("{nazwa},{email},{password}")]
        public void CreateProduct(string nazwa, string email, string pass)
        {
            _context.user.Add(new User { Name = nazwa, Email = email, Password = pass});
            _context.SaveChangesAsync();
        }

        [HttpPut("{id},{nazwa},{email},{password}")]
        public void UpdateProduct(int id, string nazwa, string email, string pass)
        {
            _context.user.FirstOrDefault(x=>x.Id==id).Name = nazwa;
            _context.user.FirstOrDefault(x => x.Id == id).Email = email;
            _context.user.FirstOrDefault(x => x.Id == id).Password = pass;
            _context.SaveChangesAsync();
        }

        [HttpDelete("{id}")]
        public async Task<IActionResult> DeleteProduct(int id)
        {
            var product = await _context.user.FindAsync(id);
            if (product == null)
            {
                return NotFound();
            }
            _context.user.Remove(product);
            await _context.SaveChangesAsync();
            return NoContent();
        }
    }
}
