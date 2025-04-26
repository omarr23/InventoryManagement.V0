using InventoryManagement.BLL.manager.InventoryProductService;
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
            return CreatedAtAction(nameof(GetInventoryProduct), new { inventoryId = inventoryProduct.InventoryId, productId = inventoryProduct.ProductId }, inventoryProduct);
        }

        // GET: api/InventoryProduct/5/10
        [HttpGet("{inventoryId:int}/{productId:int}")]
        public async Task<ActionResult<InventoryProduct>> GetInventoryProduct(int inventoryId, int productId)
        {
            var inventoryProduct = await _context.InventoryProducts
                .Include(ip => ip.Inventory)
                .Include(ip => ip.Product)
                .FirstOrDefaultAsync(ip => ip.InventoryId == inventoryId && ip.ProductId == productId);

            if (inventoryProduct == null)
            {
                return NotFound();
            }

            return Ok(inventoryProduct);
        }

        // PUT: api/InventoryProduct/5/10
        [HttpPut("{inventoryId:int}/{productId:int}")]
        public async Task<IActionResult> UpdateInventoryProduct(int inventoryId, int productId, InventoryProduct inventoryProduct)
        {
            if (inventoryId != inventoryProduct.InventoryId || productId != inventoryProduct.ProductId)
            {
                return BadRequest();
            }

            _context.Entry(inventoryProduct).State = EntityState.Modified;
            await _context.SaveChangesAsync();
            return NoContent();
        }

        // DELETE: api/InventoryProduct/5/10
        [HttpDelete("{inventoryId:int}/{productId:int}")]
        public async Task<IActionResult> DeleteInventoryProduct(int inventoryId, int productId)
        {
            var inventoryProduct = await _context.InventoryProducts
                .FirstOrDefaultAsync(ip => ip.InventoryId == inventoryId && ip.ProductId == productId);

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