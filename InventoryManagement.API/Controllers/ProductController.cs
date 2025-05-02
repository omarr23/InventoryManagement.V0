using InventoryManagement.BLL.manager.ProductService;
using InventoryManagement.BLL.manager.services;
using Microsoft.AspNetCore.Mvc;

namespace InventoryManagement.API.Controllers
{
    // ProductController.cs
    [ApiController]
    [Route("api/[controller]")]
    public class ProductController : ControllerBase
    {
        private readonly IProductService _service;

        public ProductController(IProductService service)
        {
            _service = service;
        }

        [HttpGet]
        public async Task<IActionResult> GetAll()
        {
            var products = await _service.GetAllAsync();
            return Ok(products);
        }

        [HttpGet("{id}")]
        public async Task<IActionResult> GetById(int id)
        {
            var product = await _service.GetByIdAsync(id);
            if (product == null) return NotFound();
            return Ok(product);
        }

        [HttpPost]
        public async Task<IActionResult> Add(Product product)
        {
            await _service.AddAsync(product);
            return CreatedAtAction(nameof(GetById), new { id = product.ProductId }, product);
        }

        [HttpPut("{id}")]
        public async Task<IActionResult> Update(int id, Product product)
        {
            if (id != product.ProductId)
                return BadRequest("ID mismatch");

            await _service.UpdateAsync(product);
            return NoContent();
        }

        [HttpDelete("{id}")]
        public async Task<IActionResult> Delete(int id)
        {
            var product = await _service.GetByIdAsync(id);
            if (product == null) return NotFound();

            await _service.DeleteAsync(product);
            return NoContent();
        }
    }


}
