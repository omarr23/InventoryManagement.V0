using InventoryManagement.BLL.manager.PaymentService;
using InventoryManagement.BLL.manager.services;
using Microsoft.AspNetCore.Mvc;
namespace InventoryManagement.API.Controllers
{
    

    [ApiController]
    [Route("api/[controller]")]
    public class PaymentController : ControllerBase
    {
        private readonly IPaymentService _paymentService;

        public PaymentController(IPaymentService paymentService)
        {
            _paymentService = paymentService;
        }

        [HttpGet]
        public async Task<IActionResult> GetAll()
        {
            var payments = await _paymentService.GetAllAsync();
            return Ok(payments);
        }

        [HttpGet("{id}")]
        public async Task<IActionResult> GetById(int id)
        {
            var payment = await _paymentService.GetByIdAsync(id);
            if (payment == null) return NotFound();
            return Ok(payment);
        }

        [HttpPost]
        public async Task<IActionResult> Create(Payment payment)
        {
            await _paymentService.AddAsync(payment);
            return CreatedAtAction(nameof(GetById), new { id = payment.PaymentId }, payment);
        }

        [HttpPut("{id}")]
        public async Task<IActionResult> Update(int id, Payment payment)
        {
            if (id != payment.PaymentId) return BadRequest("ID mismatch");

            await _paymentService.UpdateAsync(payment);
            return NoContent();
        }

        [HttpDelete("{id}")]
        public async Task<IActionResult> Delete(int id)
        {
            var payment = await _paymentService.GetByIdAsync(id);
            if (payment == null) return NotFound();

            await _paymentService.DeleteAsync(payment);
            return NoContent();
        }
    }

}
