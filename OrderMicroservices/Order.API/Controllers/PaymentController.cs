using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Order.ApplicationCore.Contracts.Services;
using Order.ApplicationCore.Entities;

namespace Order.API.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class PaymentController : ControllerBase
    {
        private readonly IPaymentService _paymentService;
        public PaymentController(IPaymentService paymentService)
        {
            _paymentService = paymentService;
        }

        [HttpGet("GetPaymentByCustomerId")]
        public ActionResult<IEnumerable<PaymentMethod>> GetPaymentByCustomerId([FromQuery] int customerId)
        {
            if (customerId <= 0)
                return BadRequest("invlaid customerId");

            var payments = _paymentService.GetPaymentByCustomerId(customerId);
            return Ok(payments);
        }

        [HttpPost("SavePayment")]
        public ActionResult<PaymentMethod> SavePayment([FromBody] PaymentMethod model)
        {
            if (!ModelState.IsValid)
                return ValidationProblem(ModelState);

            var created = _paymentService.SavePayment(model);
            return CreatedAtAction(nameof(GetPaymentByCustomerId),
                                   new { customerId = created.CustomerId },
                                   created);
        }

        [HttpPut("UpdatePayment")]
        public IActionResult UpdatePayment([FromBody] PaymentMethod model)
        {
            if (!ModelState.IsValid)
                return ValidationProblem(ModelState);

            var updated = _paymentService.UpdatePayment(model);
            if (!updated)
                return NotFound();

            return NoContent();
        }

        [HttpDelete("DeletePayment/{id:int}")]
        public IActionResult DeletePayment([FromRoute] int id)
        {
            var deleted = _paymentService.DeletePayment(id);
            if (!deleted)
                return NotFound();

            return NoContent();
        }
    }
}
