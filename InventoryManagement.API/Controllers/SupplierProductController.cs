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
            return CreatedAtAction(nameof(GetSupplierProduct), new { id = supplierProduct.SupplierId }, supplierProduct);
        }

        // GET: api/SupplierProduct/5
        [HttpGet("{id}")]
        public async Task<ActionResult<SupplierProduct>> GetSupplierProduct(int id)
        {
            var supplierProduct = await _context.SupplierProducts
                .Include(sp => sp.Product)
                .Include(sp => sp.Supplier)
                .FirstOrDefaultAsync(sp => sp.SupplierId == id);

            if (supplierProduct == null)
            {
                return NotFound();
            }

            return Ok(supplierProduct);
        }

        // PUT: api/SupplierProduct/5
        [HttpPut("{id}")]
        public async Task<IActionResult> UpdateSupplierProduct(int id, SupplierProduct supplierProduct)
        {
            if (id != supplierProduct.SupplierId)
            {
                return BadRequest();
            }

            _context.Entry(supplierProduct).State = EntityState.Modified;
            await _context.SaveChangesAsync();
            return NoContent();
        }

        // DELETE: api/SupplierProduct/5
        [HttpDelete("{id}")]
        public async Task<IActionResult> DeleteSupplierProduct(int id)
        {
            var supplierProduct = await _context.SupplierProducts.FindAsync(id);
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
