using InventoryManagement.DAL;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;

namespace InventoryManagement.API.Controllers
{
    [ApiController]
    [Route("api/[controller]")]
    public class SupplierProductController : ControllerBase
    {
        private readonly InventoryDbContext _context;

        public SupplierProductController(InventoryDbContext context)
        {
            _context = context;
        }

        // POST: api/SupplierProduct
        [HttpPost]
        public async Task<IActionResult> AddSupplierProduct(SupplierProduct supplierProduct)
        {
            await _context.SupplierProducts.AddAsync(supplierProduct);
            await _context.SaveChangesAsync();
            return CreatedAtAction(nameof(GetSupplierProduct), new { supplierId = supplierProduct.SupplierId, productId = supplierProduct.ProductId }, supplierProduct);
        }

        // GET: api/SupplierProduct/5/10
        [HttpGet("{supplierId:int}/{productId:int}")]
        public async Task<ActionResult<SupplierProduct>> GetSupplierProduct(int supplierId, int productId)
        {
            var supplierProduct = await _context.SupplierProducts
                .Include(sp => sp.Product)
                .Include(sp => sp.Supplier)
                .FirstOrDefaultAsync(sp => sp.SupplierId == supplierId && sp.ProductId == productId);

            if (supplierProduct == null)
            {
                return NotFound();
            }

            return Ok(supplierProduct);
        }

        // PUT: api/SupplierProduct/5/10
        [HttpPut("{supplierId:int}/{productId:int}")]
        public async Task<IActionResult> UpdateSupplierProduct(int supplierId, int productId, SupplierProduct supplierProduct)
        {
            if (supplierId != supplierProduct.SupplierId || productId != supplierProduct.ProductId)
            {
                return BadRequest();
            }

            _context.Entry(supplierProduct).State = EntityState.Modified;
            await _context.SaveChangesAsync();
            return NoContent();
        }

        // DELETE: api/SupplierProduct/5/10
        [HttpDelete("{supplierId:int}/{productId:int}")]
        public async Task<IActionResult> DeleteSupplierProduct(int supplierId, int productId)
        {
            var supplierProduct = await _context.SupplierProducts
                .FirstOrDefaultAsync(sp => sp.SupplierId == supplierId && sp.ProductId == productId);

            if (supplierProduct == null)
            {
                return NotFound();
            }

            _context.SupplierProducts.Remove(supplierProduct);
            await _context.SaveChangesAsync();
            return NoContent();
        }
    }
}
