using InventoryManagement.BLL.manager.services;
using Microsoft.AspNetCore.Mvc;

namespace InventoryManagement.API.Controllers
{
    [ApiController]
    [Route("api/[controller]")]
    public class InventoryController : ControllerBase
    {
        private readonly IGenericService<Inventory> _service;

        public InventoryController(IGenericService<Inventory> service)
        {
            _service = service;
        }

        [HttpGet]
        public async Task<IActionResult> GetAll()
        {
            var inventories = await _service.GetAllAsync();
            return Ok(inventories);
        }

        [HttpGet("{id}")]
        public async Task<IActionResult> GetById(int id)
        {
            var inventory = await _service.GetByIdAsync(id);
            if (inventory == null)
                return NotFound();
            return Ok(inventory);
        }

        [HttpPost]
        public async Task<IActionResult> Add(Inventory inventory)
        {
            // Validation based on OwnerType and OwnerId logic (e.g. validating if it's 'USER' or 'COMPANY')
            if (inventory.OwnerType != "USER" && inventory.OwnerType != "COMPANY")
                return BadRequest("Invalid OwnerType");

            await _service.AddAsync(inventory);
            return CreatedAtAction(nameof(GetById), new { id = inventory.InventoryId }, inventory);
        }

        [HttpPut("{id}")]
        public async Task<IActionResult> Update(int id, Inventory inventory)
        {
            if (id != inventory.InventoryId)
                return BadRequest("ID mismatch");

            // You can add any specific validation or rules here as well
            if (inventory.OwnerType != "USER" && inventory.OwnerType != "COMPANY")
                return BadRequest("Invalid OwnerType");

            await _service.UpdateAsync(inventory);
            return NoContent();
        }

        [HttpDelete("{id}")]
        public async Task<IActionResult> Delete(int id)
        {
            var inventory = await _service.GetByIdAsync(id);
            if (inventory == null)
                return NotFound();

            await _service.DeleteAsync(inventory);
            return NoContent();
        }
    }
}
