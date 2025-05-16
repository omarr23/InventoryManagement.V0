using InventoryManagement.BLL.DTO.PaymentDTO;
using InventoryManagement.BLL.manager.PaymentService;
using InventoryManagement.BLL.manager.services;
using Microsoft.AspNetCore.Mvc;
namespace InventoryManagement.API.Controllers
{using Microsoft.AspNetCore.Authorization;
    using static InventoryManagement.BLL.DTO.PaymentDTO.PaymentDTO;

    [ApiController]
    [Route("api/[controller]")]
    [Authorize] // Uncomment if you want to require authentication for this controller
    public class PaymentsController : ControllerBase
    {
        private readonly IPaymentService _paymentService;

        public PaymentsController(IPaymentService paymentService)
        {
            _paymentService = paymentService;
        }


        // Get all payments
        [HttpGet]
        public async Task<IActionResult> GetAllPayments()
        {
            var result = await _paymentService.GetAllPaymentsAsync();

            if (!result.IsSuccess)
                return BadRequest(new { message = result.Error });

            return Ok(result.Value);
        }

        // Get payment by id
        [HttpGet("{id}")]
        public async Task<IActionResult> GetPaymentById(int id)
        {
            var result = await _paymentService.GetPaymentByIdAsync(id);

            if (!result.IsSuccess)
                return NotFound(new { message = result.Error });

            if (result.Value == null)
                return NotFound(new { message = "Payment not found." });

            return Ok(result.Value);
        }

        // Create new payment
        [HttpPost]
        public async Task<IActionResult> CreatePayment([FromBody] CreatePaymentDto dto)
        {
            if (!ModelState.IsValid)
                return BadRequest(ModelState);

            var result = await _paymentService.CreatePaymentAsync(dto);

            if (!result.IsSuccess)
                return BadRequest(new { message = result.Error });

            return CreatedAtAction(nameof(GetPaymentById), new { id = result.Value.PaymentId }, result.Value);
        }

        // Update payment
        [HttpPut("{id}")]
        public async Task<IActionResult> UpdatePayment(int id, [FromBody] UpdatePaymentDto dto)
        {
            if (!ModelState.IsValid)
                return BadRequest(ModelState);

            var result = await _paymentService.UpdatePaymentAsync(id, dto);

            if (!result.IsSuccess)
                return NotFound(new { message = result.Error });

            return NoContent();
        }

        // Delete payment
        [HttpDelete("{id}")]
        public async Task<IActionResult> DeletePayment(int id)
        {
            var result = await _paymentService.DeletePaymentAsync(id);

            if (!result.IsSuccess)
                return NotFound(new { message = result.Error });

            return NoContent();
        }
    }

}
