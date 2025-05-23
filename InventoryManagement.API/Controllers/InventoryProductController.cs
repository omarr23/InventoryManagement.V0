﻿using InventoryManagement.BLL.DTO.InventoryProductDTO;
using InventoryManagement.BLL.manager.InventoryProductService;
using InventoryManagement.DAL;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using Microsoft.AspNetCore.Authorization;
namespace InventoryManagement.API.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    [Authorize] // Uncomment if you want to require authentication for this controller
    public class InventoryProductController : ControllerBase
    {
        private readonly IInventoryProductService _service;

        public InventoryProductController(IInventoryProductService service)
        {
            _service = service;
        }

        // GET: api/InventoryProduct
        [HttpGet]
        public async Task<ActionResult<IEnumerable<InventoryProductReadDTO>>> GetAll()
        {
            var result = await _service.GetAllAsync();
            if (!result.IsSuccess)
                return StatusCode(500, result.Error);

            return Ok(result.Value);
        }

        // GET: api/InventoryProduct/{inventoryId}/{productId}
        [HttpGet("{inventoryId}/{productId}")]
        public async Task<ActionResult<InventoryProductReadDTO>> GetById(int inventoryId, int productId)
        {
            var result = await _service.GetByIdAsync(inventoryId, productId);
            if (!result.IsSuccess)
                return NotFound(result.Error);

            return Ok(result.Value);
        }

        // POST: api/InventoryProduct
        [HttpPost("{inventoryId}")]
        public async Task<ActionResult> Add(int inventoryId, [FromBody] CreateInventoryProductDTO dto)
        {
            if (!ModelState.IsValid)
            {
                return BadRequest(ModelState); // If the model is invalid, return a bad request with validation errors
            }

            var result = await _service.AddAsync(dto, inventoryId);
            if (!result.IsSuccess)
                return BadRequest(result.Error);

            // Modify CreatedAtAction to include both inventoryId and productId
            //return CreatedAtAction(nameof(GetById), new {inventoryId =inventoryId  ,productId = dto.ProductId }, dto);

            return CreatedAtAction(nameof(GetById), new { inventoryId = inventoryId, productId = dto.ProductId }, dto);
        }
        // PUT: api/InventoryProduct/{inventoryId}/{productId}
        [HttpPut("{inventoryId}/{productId}")]
        public async Task<ActionResult> Update(int inventoryId, int productId, [FromBody] UpdateInventoryProductDTO dto)
        {
            if (!ModelState.IsValid)
            {
                return BadRequest(ModelState); // If the model is invalid, return a bad request with validation errors
            }

            var result = await _service.UpdateAsync(inventoryId, productId, dto);
            if (!result.IsSuccess)
                return NotFound(result.Error);

            return NoContent();
        }

        // DELETE: api/InventoryProduct/{inventoryId}/{productId}
        [HttpDelete("{inventoryId}/{productId}")]
        public async Task<ActionResult> Delete(int inventoryId, int productId)
        {
            var result = await _service.DeleteAsync(inventoryId, productId);
            if (!result.IsSuccess)
                return NotFound(result.Error);

            return NoContent();
        }
    }
}