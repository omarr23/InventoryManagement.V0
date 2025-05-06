using InventoryManagement.BLL.manager.SupplierProductService;
using InventoryManagement.DAL;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using static InventoryManagement.BLL.DTO.SupplierProductDTO.SupplierPrpductDTO;
using Microsoft.AspNetCore.Authorization;

namespace InventoryManagement.API.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    [Authorize] // Uncomment if you want to require authentication for this controller
    public class SupplierProductController : ControllerBase
    {
        private readonly ISupplierProductService _supplierProductService;

        // Constructor with dependency injection of ISupplierProductService
        public SupplierProductController(ISupplierProductService supplierProductService)
        {
            _supplierProductService = supplierProductService;
        }

        // POST api/supplierproduct
        [HttpPost]
        public async Task<IActionResult> AddSupplierProductAsync([FromBody] CreateSupplierProductDTO dto)
        {
            try
            {
                await _supplierProductService.AddSupplierProductAsync(dto);


                return CreatedAtRoute(
                    "GetSupplierProduct",
                    new { supplierId = dto.SupplierId, productId = dto.ProductId },
                    value: dto);
            }
            catch (InvalidOperationException ex)
            {
                return BadRequest(ex.Message);  // Handle already existing relation
            }
            catch (Exception ex)
            {
                return StatusCode(500, $"Internal server error: {ex.Message}");
            }
        }



        // GET api/supplierproduct/{supplierId}/{productId}
        [HttpGet("{supplierId}/{productId}", Name = "GetSupplierProduct")]
        public async Task<IActionResult> GetSupplierProductByIdAsync(int supplierId, int productId)
        {
            var supplierProduct = await _supplierProductService.GetSupplierProductByIdAsync(supplierId, productId);
            if (supplierProduct == null)
            {
                return NotFound($"SupplierProduct with SupplierId {supplierId} and ProductId {productId} not found.");
            }
            return Ok(supplierProduct);
        }



        // PUT api/supplierproduct
        [HttpPut]
        public async Task<IActionResult> UpdateSupplierProductAsync([FromBody] UpdateSupplierProductDTO dto)
        {
            try
            {
                await _supplierProductService.UpdateSupplierProductAsync(dto);
                return NoContent();  // Return no content if the update is successful
            }
            catch (InvalidOperationException ex)
            {
                return NotFound(ex.Message);  // Handle not found exception
            }
            catch (Exception ex)
            {
                return StatusCode(500, $"Internal server error: {ex.Message}");
            }
        }

        // DELETE api/supplierproduct/{supplierId}/{productId}
        [HttpDelete("{supplierId}/{productId}")]
        public async Task<IActionResult> DeleteSupplierProductAsync(int supplierId, int productId)
        {
            try
            {
                await _supplierProductService.DeleteSupplierProductAsync(supplierId, productId);
                return NoContent();  // Return no content if the delete is successful
            }
            catch (InvalidOperationException ex)
            {
                return NotFound(ex.Message);  // Handle not found exception
            }
            catch (Exception ex)
            {
                return StatusCode(500, $"Internal server error: {ex.Message}");
            }
        }
    }
}
