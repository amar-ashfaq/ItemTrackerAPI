using AutoMapper;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;

namespace ItemTrackerAPI.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class ItemController : ControllerBase
    {
        private readonly ItemDbContext _context;
        private readonly IMapper _mapper;

        public ItemController(ItemDbContext context, IMapper mapper)
        {
            _context = context;
            _mapper = mapper;
        }

        // GET ALL: api/Items
        [HttpGet]
        public async Task<ActionResult<IEnumerable<ItemReadDto>>> GetItems()
        {
            var items = await _context.Items.ToListAsync();
            var itemsDto = _mapper.Map<List<ItemReadDto>>(items);
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

            var itemDto = _mapper.Map<ItemReadDto>(item);
         
            return Ok(itemDto);
        }

        // POST: api/Item
        [HttpPost]
        public async Task<IActionResult> CreateItem(ItemCreateDto itemDto)
        {
            if (itemDto == null) {
               return BadRequest();
            }

            var item = _mapper.Map<ItemEntity>(itemDto);

            await _context.Items.AddAsync(item);
            await _context.SaveChangesAsync();

            var itemReadDto = _mapper.Map<ItemReadDto>(item);

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

            _mapper.Map(itemDto, existing);

            await _context.SaveChangesAsync();

            return NoContent();
        }
    }
}
