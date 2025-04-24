using InventoryManagement.BLL.manager.services;
using Microsoft.AspNetCore.Mvc;

namespace InventoryManagement.API.Controllers
{
    [ApiController]
    [Route("api/[controller]")]
    public class PaymentController : ControllerBase
    {
        private readonly IGenericService<Payment> _service;

        public PaymentController(IGenericService<Payment> service)
        {
            _service = service;
        }

        [HttpGet]
        public async Task<IActionResult> GetAll()
        {
            var payments = await _service.GetAllAsync();
            return Ok(payments);
        }

        [HttpGet("{id}")]
        public async Task<IActionResult> GetById(int id)
        {
            var payment = await _service.GetByIdAsync(id);
            if (payment == null)
                return NotFound();
            return Ok(payment);
        }

        [HttpPost]
        public async Task<IActionResult> Add(Payment payment)
        {
            // Add any specific validations or checks for the payment method or status if needed
            if (payment.Amount <= 0)
                return BadRequest("Amount must be greater than zero");

            payment.PaymentDate = DateTime.UtcNow;  // Optional: Set PaymentDate to current UTC time
            await _service.AddAsync(payment);
            return CreatedAtAction(nameof(GetById), new { id = payment.PaymentId }, payment);
        }

        [HttpPut("{id}")]
        public async Task<IActionResult> Update(int id, Payment payment)
        {
            if (id != payment.PaymentId)
                return BadRequest("ID mismatch");

            await _service.UpdateAsync(payment);
            return NoContent();
        }

        [HttpDelete("{id}")]
        public async Task<IActionResult> Delete(int id)
        {
            var payment = await _service.GetByIdAsync(id);
            if (payment == null)
                return NotFound();

            await _service.DeleteAsync(payment);
            return NoContent();
        }
    }
}
