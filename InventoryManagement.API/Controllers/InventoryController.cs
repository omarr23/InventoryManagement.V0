using InventoryManagement.BLL.manager.InventoryService;
using InventoryManagement.BLL.manager.services;
using Microsoft.AspNetCore.Mvc;
using static InventoryManagement.BLL.DTO.InventoryDTO.InventoryDTO;

namespace InventoryManagement.API.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
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
            return Ok(inventories); // Return all inventories with products
        }

        // GET: api/Inventory/{id}
        [HttpGet("{id}")]
        public async Task<ActionResult<InventoryReadDTO>> GetById(int id)
        {
            var inventory = await _inventoryService.GetByIdAsync(id);
            if (inventory == null)
                return NotFound($"Inventory with ID {id} not found.");

            return Ok(inventory); // Return the inventory with products by ID
        }

        // POST: api/Inventory
        [HttpPost]
        public async Task<ActionResult> Create([FromBody] CreateInventoryDTO createInventoryDTO)
        {
            if (!ModelState.IsValid)
                return BadRequest(ModelState);

            await _inventoryService.AddAsync(createInventoryDTO);
            return CreatedAtAction(nameof(GetById), new { id = createInventoryDTO.OwnerId }, createInventoryDTO);
        }

        // PUT: api/Inventory/{id}
        [HttpPut("{id}")]
        public async Task<ActionResult> Update(int id, [FromBody] UpdateInventoryDTO updateInventoryDTO)
        {
            if (!ModelState.IsValid)
                return BadRequest(ModelState);

            try
            {
                await _inventoryService.UpdateAsync(id, updateInventoryDTO);
            }
            catch (KeyNotFoundException)
            {
                return NotFound($"Inventory with ID {id} not found.");
            }

            return NoContent(); // Return 204 No Content after successful update
        }

        // DELETE: api/Inventory/{id}
        [HttpDelete("{id}")]
        public async Task<ActionResult> Delete(int id)
        {
            try
            {
                await _inventoryService.DeleteAsync(id);
            }
            catch (KeyNotFoundException)
            {
                return NotFound($"Inventory with ID {id} not found.");
            }

            return NoContent(); // Return 204 No Content after successful deletion
        }
    }
}
