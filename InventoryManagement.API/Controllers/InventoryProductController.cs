
using InventoryManagement.DAL;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;

namespace InventoryManagement.API.Controllers
{
    [ApiController]
    [Route("api/[controller]")]
    public class InventoryProductController : ControllerBase
    {
        private readonly InventoryDbContext _context;

        public InventoryProductController(InventoryDbContext context)
        {
            _context = context;
        }

        // POST: api/InventoryProduct
        [HttpPost]
        public async Task<IActionResult> AddInventoryProduct(InventoryProduct inventoryProduct)
        {
            await _context.InventoryProducts.AddAsync(inventoryProduct);
            await _context.SaveChangesAsync();
            return CreatedAtAction(nameof(GetInventoryProduct), new { id = inventoryProduct.InventoryId }, inventoryProduct);
        }

        // GET: api/InventoryProduct/5
        [HttpGet("{id}")]
        public async Task<ActionResult<InventoryProduct>> GetInventoryProduct(int id)
        {
            var inventoryProduct = await _context.InventoryProducts
                .Include(ip => ip.Inventory)
                .Include(ip => ip.Product)
                .FirstOrDefaultAsync(ip => ip.InventoryId == id);

            if (inventoryProduct == null)
            {
                return NotFound();
            }

            return Ok(inventoryProduct);
        }

        // PUT: api/InventoryProduct/5
        [HttpPut("{id}")]
        public async Task<IActionResult> UpdateInventoryProduct(int id, InventoryProduct inventoryProduct)
        {
            if (id != inventoryProduct.InventoryId)
            {
                return BadRequest();
            }

            _context.Entry(inventoryProduct).State = EntityState.Modified;
            await _context.SaveChangesAsync();
            return NoContent();
        }

        // DELETE: api/InventoryProduct/5
        [HttpDelete("{id}")]
        public async Task<IActionResult> DeleteInventoryProduct(int id)
        {
            var inventoryProduct = await _context.InventoryProducts.FindAsync(id);
            if (inventoryProduct == null)
            {
                return NotFound();
            }

            _context.InventoryProducts.Remove(inventoryProduct);
            await _context.SaveChangesAsync();
            return NoContent();
        }
    }

}
