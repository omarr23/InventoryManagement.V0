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

        public SupplierProductController(ISupplierProductService supplierProductService)
        {
            _supplierProductService = supplierProductService;
        }

        // POST api/supplierproduct
        [HttpPost]
        public async Task<IActionResult> AddSupplierProductAsync([FromBody] CreateSupplierProductDTO dto)
        {
            var result = await _supplierProductService.AddSupplierProductAsync(dto);

            if (!result.IsSuccess)
            {
                return BadRequest(result.ErrorMessage);  // Handle already existing relation or other errors
            }

            return CreatedAtRoute(
                "GetSupplierProduct",
                new { supplierId = dto.SupplierId, productId = dto.ProductId },
                value: dto);
        }

        // GET api/supplierproduct/{supplierId}/{productId}
        [HttpGet("{supplierId}/{productId}", Name = "GetSupplierProduct")]
        public async Task<IActionResult> GetSupplierProductByIdAsync(int supplierId, int productId)
        {
            var result = await _supplierProductService.GetSupplierProductByIdAsync(supplierId, productId);

            if (result.IsSuccess)
            {
                return Ok(result.Value);
            }

            return NotFound(result.ErrorMessage);
        }

        // PUT api/supplierproduct
        [HttpPut]
        public async Task<IActionResult> UpdateSupplierProductAsync([FromBody] UpdateSupplierProductDTO dto)
        {
            var result = await _supplierProductService.UpdateSupplierProductAsync(dto);

            if (!result.IsSuccess)
            {
                return NotFound(result.ErrorMessage);  // Handle not found exception
            }

            return NoContent();  // Return no content if the update is successful
        }

        // DELETE api/supplierproduct/{supplierId}/{productId}
        [HttpDelete("{supplierId}/{productId}")]
        public async Task<IActionResult> DeleteSupplierProductAsync(int supplierId, int productId)
        {
            var result = await _supplierProductService.DeleteSupplierProductAsync(supplierId, productId);

            if (!result.IsSuccess)
            {
                return NotFound(result.ErrorMessage);  // Handle not found exception
            }

            return NoContent();  // Return no content if the delete is successful
        }
    }
}