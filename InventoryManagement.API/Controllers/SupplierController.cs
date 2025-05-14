using InventoryManagement.BLL.manager.services;
using InventoryManagement.BLL.manager.SupplierService;
using InventoryManagement.BLL.Mappers;
using Microsoft.AspNetCore.Mvc;
using static InventoryManagement.BLL.DTO.SupplierDTO.SupplierDTO;
using Microsoft.AspNetCore.Authorization;

namespace InventoryManagement.API.Controllers
{
    [ApiController]
    [Route("api/[controller]")]
    [Authorize] // Uncomment if needed
    public class SupplierController : ControllerBase
    {
        private readonly ISupplierService _service;

        public SupplierController(ISupplierService service)
        {
            _service = service;
        }

        [HttpGet]
        public async Task<IActionResult> GetAll()
        {
            var result = await _service.GetAllAsync();
            if (!result.IsSuccess)
                return BadRequest(result.Error);

            return Ok(result.Value);
        }

        [HttpGet("{id}")]
        public async Task<IActionResult> GetById(int id)
        {
            var result = await _service.GetByIdAsync(id);
            if (!result.IsSuccess)
                return NotFound(result.Error);

            return Ok(result.Value);
        }

        [HttpPost]
        public async Task<IActionResult> Add([FromBody] SupplierCreateDTO dto)
        {
            if (dto == null)
                return BadRequest("Invalid supplier data.");

            var result = await _service.AddAsync(dto);
            if (!result.IsSuccess)
                return BadRequest(result.Error);

            return CreatedAtAction(nameof(GetById), new { id = result.Value.SupplierId }, result.Value);
        }

        [HttpPut("{id}")]
        public async Task<IActionResult> Update(int id, [FromBody] SupplierUpdateDTO dto)
        {
            if (dto == null || id != dto.SupplierId)
                return BadRequest("ID mismatch.");

            var result = await _service.UpdateAsync(id, dto);
            if (!result.IsSuccess)
                return NotFound(result.Error);

            return NoContent();
        }

        [HttpDelete("{id}")]
        public async Task<IActionResult> Delete(int id)
        {
            var result = await _service.DeleteAsync(id);
            if (!result.IsSuccess)
                return NotFound(result.Error);

            return NoContent();
        }
    }


}
