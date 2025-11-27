using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;

namespace ItemTrackerAPI.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class ItemController : ControllerBase
    {
        private readonly ItemDbContext _context;

        public ItemController(ItemDbContext context)
        {
            _context = context;
        }

        // GET ALL: api/Items
        [HttpGet]
        public async Task<ActionResult<IEnumerable<ItemEntity>>> GetItems()
        {
            var items = await _context.Items.ToListAsync();
            return Ok(items);
        }

        // GET: api/Item/3
        [HttpGet("{id}")]
        public async Task<ActionResult<ItemEntity>> GetItem(int id) {

            var item = await _context.Items.FindAsync(id);

            if (item == null) {
                return NotFound();
            }
            return Ok(item);
        }

        // POST: api/Item
        [HttpPost]
        public async Task<IActionResult> CreateItem(ItemEntity item)
        {
            if (item == null) {
               return BadRequest();
            }

            await _context.Items.AddAsync(item);
            await _context.SaveChangesAsync();

            return CreatedAtAction(nameof(GetItem), new { id = item.Id }, item);
        }

        // DELETE: api/Item/5
        [HttpDelete("{id}")]
        public async Task<IActionResult> DeleteItem(int id) { 
        
            var item = await _context.Items.FindAsync(id);

            if (item == null)
            {
                return NotFound();
            }

            _context.Items.Remove(item);
            await _context.SaveChangesAsync();

            return NoContent();
        }

        // DELETE ALL: api/Items
        [HttpDelete]
        public async Task<IActionResult> DeleteItems()
        {
            if (!await _context.Items.AnyAsync())
            {
                return NotFound();
            }

            _context.Items.RemoveRange(_context.Items);
            await _context.SaveChangesAsync();

            return NoContent();
        }

        // PUT: api/Item/2
        [HttpPut("{id}")]
        public async Task<IActionResult> UpdateItem(int id, ItemEntity item) {

            if (id != item.Id)
            {
                return BadRequest();
            }

            var existing = await _context.Items.FindAsync(id);

            if (existing == null)
            {
                return NotFound();
            }

            existing.Name = item.Name;
            existing.Description = item.Description;

            await _context.SaveChangesAsync();

            return NoContent();
        }
    }
}
