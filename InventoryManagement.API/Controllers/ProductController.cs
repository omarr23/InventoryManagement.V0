using InventoryManagement.BLL.DTO.ProductDTO;
using InventoryManagement.BLL.manager.ProductService;
using InventoryManagement.BLL.manager.services;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Authorization;

namespace InventoryManagement.API.Controllers
{
    // ProductController.cs
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

        [HttpGet]
        public async Task<IActionResult> GetAll([FromQuery] PaginationParameters parameters)
        {
            var paginatedResult = await _service.GetPaginatedAsync(parameters);
            return Ok(paginatedResult);
        }

        [HttpGet("all")]
        public async Task<IActionResult> GetAllWithoutPagination()
        {
            var products = await _service.GetAllAsync();
            return Ok(products);
        }

        [HttpGet("{id}")]
        public async Task<IActionResult> GetById(int id)
        {
            var product = await _service.GetByIdAsync(id);
            if (product == null)
                return NotFound();

            return Ok(product);
        }

        [HttpPost]
        public async Task<IActionResult> Add(ProductDTO.ProductCreatDTO dto)
        {
            await _service.AddAsync(dto);
            return Ok(); // Or use CreatedAtAction if returning resource
        }

        [HttpPut("{id}")]
        public async Task<IActionResult> Update(int id, ProductDTO.ProductUpdateDTO dto)
        {
            try
            {
                await _service.UpdateAsync(id, dto);
                return NoContent();
            }
            catch (KeyNotFoundException)
            {
                return NotFound();
            }
        }

        [HttpDelete("{id}")]
        public async Task<IActionResult> Delete(int id)
        {
            try
            {
                await _service.DeleteAsync(id);
                return NoContent();
            }
            catch (KeyNotFoundException)
            {
                return NotFound();
            }
        }


    }
}
