using InventoryManagement.BLL.DTO.ProductDTO;
using InventoryManagement.BLL.manager.ProductService;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Authorization;
using static InventoryManagement.BLL.DTO.ProductDTO.ProductDTO;

namespace InventoryManagement.API.Controllers
{
    [ApiController]
    [Route("api/[controller]")]
    [Authorize] // Uncomment if you want to require authentication for this controller
    public class ProductController : ControllerBase
    {
        private readonly IProductService _service;

        public ProductController(IProductService service)
        {
            _service = service;
        }

        // Get paginated list of products
        [HttpGet]
        public async Task<IActionResult> GetAll([FromQuery] PaginationParameters parameters)
        {
            var result = await _service.GetPaginatedAsync(parameters);
            if (!result.IsSuccess)
                return BadRequest(result.Error);

            return Ok(result.Value);
        }

        // Get all products without pagination
        [HttpGet("all")]
        public async Task<IActionResult> GetAllWithoutPagination()
        {
            var result = await _service.GetAllAsync();
            if (!result.IsSuccess)
                return BadRequest(result.Error);

            return Ok(result.Value);
        }

        // Get soft-deleted products
        [HttpGet("inactive")]
        public async Task<IActionResult> GetSoftDeleted()
        {
            var result = await _service.GetSoftDeletedAsync();
            if (!result.IsSuccess)
                return BadRequest(result.Error);

            return Ok(result.Value);
        }

        // Get a product by ID
        [HttpGet("{id}")]
        public async Task<IActionResult> GetById(int id)
        {
            var result = await _service.GetByIdAsync(id);
            if (!result.IsSuccess)
                return NotFound(result.Error);

            return Ok(result.Value);
        }

        // Add a new product
        [HttpPost]
        public async Task<IActionResult> Add([FromBody] ProductCreatDTO dto)
        {
            if (!ModelState.IsValid)
                return BadRequest(ModelState);

            var result = await _service.AddAsync(dto);
            if (!result.IsSuccess)
                return BadRequest(result.Error);

            return CreatedAtAction(nameof(GetById), new { id = result.Value.ProductId }, result.Value);
        }

        // Update an existing product
        [HttpPut("{id}")]
        public async Task<IActionResult> Update(int id, [FromBody] ProductUpdateDTO dto)
        {
            try
            {
                var result = await _service.UpdateAsync(id, dto);
                if (!result.IsSuccess)
                    return NotFound(result.Error);

                return NoContent();
            }
            catch (KeyNotFoundException ex)
            {
                return NotFound($"Product with ID {id} not found: {ex.Message}");
            }
        }

        // Delete a product
        [HttpDelete("{id}")]
        public async Task<IActionResult> Delete(int id)
        {
            try
            {
                var result = await _service.DeleteAsync(id);
                if (!result.IsSuccess)
                    return NotFound(result.Error);

                return NoContent();
            }
            catch (KeyNotFoundException ex)
            {
                return NotFound($"Product with ID {id} not found: {ex.Message}");
            }
        }
    }
}