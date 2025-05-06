using InventoryManagement.BLL.manager.services;
using InventoryManagement.BLL.manager.SupplierService;
using InventoryManagement.BLL.Mappers;
using Microsoft.AspNetCore.Mvc;
using static InventoryManagement.BLL.DTO.SupplierDTO.SupplierDTO;


namespace InventoryManagement.API.Controllers
{
    [ApiController]
    [Route("api/[controller]")]
    public class SupplierController : ControllerBase
    {
        private readonly ISupplierService _service;

        public SupplierController(ISupplierService service)
        {
            _service = service;
        }

        // Get all suppliers
        [HttpGet]
        public async Task<IActionResult> GetAll()
        {
            var suppliers = await _service.GetAllAsync();
            return Ok(suppliers);
        }

        // Get supplier by ID
        [HttpGet("{id}")]
        public async Task<IActionResult> GetById(int id)
        {
            var supplier = await _service.GetByIdAsync(id);
            if (supplier == null)
                return NotFound();
            return Ok(supplier);
        }

        // Add a new supplier
        [HttpPost]
        [HttpPost]
        [HttpPost]
        public async Task<IActionResult> Add([FromBody] SupplierCreateDTO dto)
        {
            if (dto == null)
                return BadRequest("Invalid supplier data.");

            // Add the supplier
            await _service.AddAsync(dto);

            // Now, fetch the supplier by ID that was created by the database
            var suppliers = await _service.GetAllAsync();
            var createdSupplier = suppliers.LastOrDefault(); // Assuming it's the last supplier added

            if (createdSupplier == null)
                return NotFound();

            return CreatedAtAction(nameof(GetById), new { id = createdSupplier.SupplierId }, createdSupplier);
        }



        // Update an existing supplier
        [HttpPut("{id}")]
        public async Task<IActionResult> Update(int id, [FromBody] SupplierUpdateDTO dto)
        {
            if (dto == null || id != dto.SupplierId)
                return BadRequest("ID mismatch.");

            var supplier = await _service.GetByIdAsync(id);
            if (supplier == null)
                return NotFound();

            await _service.UpdateAsync(id, dto);
            return NoContent();
        }

        // Delete a supplier
        [HttpDelete("{id}")]
        public async Task<IActionResult> Delete(int id)
        {
            var supplier = await _service.GetByIdAsync(id);
            if (supplier == null)
                return NotFound();

            await _service.DeleteAsync(id);
            return NoContent();
        }
    }
}

