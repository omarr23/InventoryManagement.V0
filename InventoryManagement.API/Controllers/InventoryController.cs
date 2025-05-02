using InventoryManagement.BLL.manager.InventoryService;
using InventoryManagement.BLL.manager.services;
using Microsoft.AspNetCore.Mvc;

namespace InventoryManagement.API.Controllers
{
    [ApiController]
    [Route("api/[controller]")]
    public class InventoryController : ControllerBase
    {
        private readonly IInventoryService _service;

        public InventoryController(IInventoryService service)
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
            if (inventory == null) return NotFound();
            return Ok(inventory);
        }

        [HttpPost]
        public async Task<IActionResult> Add(Inventory inventory)
        {
            await _service.AddAsync(inventory);
            return CreatedAtAction(nameof(GetById), new { id = inventory.InventoryId }, inventory);
        }

        [HttpPut("{id}")]
        public async Task<IActionResult> Update(int id, Inventory inventory)
        {
            if (id != inventory.InventoryId)
                return BadRequest("ID mismatch");

            await _service.UpdateAsync(inventory);
            return NoContent();
        }

        [HttpDelete("{id}")]
        public async Task<IActionResult> Delete(int id)
        {
            var inventory = await _service.GetByIdAsync(id);
            if (inventory == null) return NotFound();

            await _service.DeleteAsync(inventory);
            return NoContent();
        }
    }
}
