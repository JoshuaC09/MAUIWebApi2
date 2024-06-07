using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using WebApplication1.DatabaseContext;
using WebApplication2.Models;
using System.Threading.Tasks;
using System.Linq;

namespace WebApplication2.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class InventoryController : ControllerBase
    {
        private readonly MyDbContext _dbContext;
        public InventoryController(MyDbContext myDbContext)
        {
            _dbContext = myDbContext;
        }

        // Create
        [HttpPost]
        public async Task<IActionResult> AddProduct(Inventory inventory)
        {
            _dbContext.Inventory.Add(inventory);
            await _dbContext.SaveChangesAsync();
            return Ok(inventory);
        }

        // Read All
        [HttpGet]
        public async Task<IActionResult> GetAll()
        {
            var inventory = await _dbContext.Inventory.ToListAsync();
            return Ok(inventory);
        }

        // Read by ID
        [HttpGet("{id}")]
        public async Task<IActionResult> GetById(int id)
        {
            var inventory = await _dbContext.Inventory.FindAsync(id);
            if (inventory == null)
            {
                return NotFound();
            }
            return Ok(inventory);
        }

        // Update
        [HttpPut("{id}")]
        public async Task<IActionResult> UpdateProduct(int id, Inventory inventory)
        {
            if (id != inventory.InventoryID)
            {
                return BadRequest();
            }

            _dbContext.Entry(inventory).State = EntityState.Modified;

            try
            {
                await _dbContext.SaveChangesAsync();
            }
            catch (DbUpdateConcurrencyException)
            {
                if (!InventoryExists(id))
                {
                    return NotFound();
                }
                else
                {
                    throw;
                }
            }

            return NoContent();
        }

        // Delete
        [HttpDelete("{id}")]
        public async Task<IActionResult> DeleteProduct(int id)
        {
            var inventory = await _dbContext.Inventory.FindAsync(id);
            if (inventory == null)
            {
                return NotFound();
            }

            _dbContext.Inventory.Remove(inventory);
            await _dbContext.SaveChangesAsync();

            return NoContent();
        }

        private bool InventoryExists(int id)
        {
            return _dbContext.Inventory.Any(e => e.InventoryID == id);
        }
    }
}
