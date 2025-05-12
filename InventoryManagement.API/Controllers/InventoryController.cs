using InventoryManagement.BLL.Helper;
using InventoryManagement.BLL.manager.InventoryService;
using InventoryManagement.BLL.manager.services;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using static InventoryManagement.BLL.DTO.InventoryDTO.InventoryDTO;

namespace InventoryManagement.API.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    [Authorize]
    public class InventoryController : ControllerBase
    {
        private readonly IInventoryService _inventoryService;

        public InventoryController(IInventoryService inventoryService)
        {
            _inventoryService = inventoryService;
        }

        // GET: api/Inventory
        [HttpGet]
        public async Task<ActionResult<IEnumerable<InventoryReadDTO>>> GetAll()
        {
            var inventories = await _inventoryService.GetAllAsync();
            if (!inventories.IsSuccess)
                return BadRequest(inventories.Error);

            return Ok(inventories.Value); // Return all inventories with products
        }

        // GET: api/Inventory/{id}
        [HttpGet("{id}")]
        public async Task<ActionResult<InventoryReadDTO>> GetById(int id)
        {
            var inventory = await _inventoryService.GetByIdAsync(id);
            if (!inventory.IsSuccess)
                return NotFound(inventory.Error);

            return Ok(inventory.Value); // Return the inventory with products by ID
        }

        // POST: api/Inventory
        [HttpPost]
        public async Task<ActionResult> Create([FromBody] CreateInventoryDTO createInventoryDTO)
        {
            if (!ModelState.IsValid)
                return BadRequest(ModelState);

            // Get the current user's ID
            var userId = User.FindFirst(System.Security.Claims.ClaimTypes.NameIdentifier)?.Value;
            if (string.IsNullOrEmpty(userId))
                return Unauthorized("User not authenticated");

            var createdInventory = await _inventoryService.AddAsync(createInventoryDTO, userId);
            if (!createdInventory.IsSuccess)
                return BadRequest(createdInventory.Error);

            return CreatedAtAction(nameof(GetById), new { id = createdInventory.Value.InventoryId }, createdInventory.Value);
        }

        // PUT: api/Inventory/{id}
        [HttpPut("{id}")]
        public async Task<ActionResult> Update(int id, [FromBody] UpdateInventoryDTO updateInventoryDTO)
        {
            if (!ModelState.IsValid)
                return BadRequest(ModelState);

            var result = await _inventoryService.UpdateAsync(id, updateInventoryDTO);

            if (!result.IsSuccess)
                return NotFound(result.Error);

            return NoContent(); // Return 204 No Content after successful update
        }

        // DELETE: api/Inventory/{id}
        [HttpDelete("{id}")]
        public async Task<ActionResult> Delete(int id)
        {
            var result = await _inventoryService.DeleteAsync(id);

            if (!result.IsSuccess)
                return NotFound(result.Error);

            return NoContent(); // Return 204 No Content after successful deletion
        }
    }
}
