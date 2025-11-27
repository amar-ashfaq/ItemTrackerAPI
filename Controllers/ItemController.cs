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
        public async Task<ActionResult<IEnumerable<ItemReadDto>>> GetItems()
        {
            var items = await _context.Items.ToListAsync();

            var itemsDto = items.Select(item => new ItemReadDto
            {
                Id = item.Id,
                Name = item.Name,
                Description = item.Description
            }).ToList();

            return Ok(itemsDto);
        }

        // GET: api/Item/3
        [HttpGet("{id}")]
        public async Task<ActionResult<ItemReadDto>> GetItem(int id) {

            var item = await _context.Items.FindAsync(id);

            if (item == null)
            {
                return NotFound();
            }

            var itemDto = new ItemReadDto
            {
                Id = item.Id,
                Name= item.Name,
                Description = item.Description
            };
         
            return Ok(itemDto);
        }

        // POST: api/Item
        [HttpPost]
        public async Task<IActionResult> CreateItem(ItemCreateDto itemDto)
        {
            if (itemDto == null) {
               return BadRequest();
            }

            var item = new ItemEntity
            {
                Name = itemDto.Name,
                Description = itemDto.Description
            };

            await _context.Items.AddAsync(item);
            await _context.SaveChangesAsync();

            var itemReadDto = new ItemReadDto
            {
                Id = item.Id,
                Name = item.Name,
                Description = item.Description
            };

            return CreatedAtAction(nameof(GetItem), new { id = item.Id }, itemReadDto);
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
        public async Task<IActionResult> UpdateItem(int id, ItemCreateDto itemDto) {

            if (itemDto == null)
            {
                return BadRequest();
            }

            var existing = await _context.Items.FindAsync(id);

            if (existing == null)
            {
                return NotFound();
            }

            existing.Name = itemDto.Name;
            existing.Description = itemDto.Description;

            await _context.SaveChangesAsync();

            return NoContent();
        }
    }
}
