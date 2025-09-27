using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Order.ApplicationCore.Contracts.Services;
using Order.ApplicationCore.Entities;

namespace Oder.API.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class OrderController : ControllerBase
    {
        private readonly IOrderService _service;
        public OrderController(IOrderService service)
        {
            _service = service;
        }

        [HttpGet]
        public IActionResult GetAll()
        {
            var orders = _service.GetAllOrders();
            return Ok(orders);
        }

        [HttpPost]
        public IActionResult Create(OrderEntity order)
        {
            var created = _service.CreateOrder(order);
            return CreatedAtAction(nameof(GetAll), new { id = created.Id }, created);
        }

        [HttpGet("by-customer/{customerId}")]
        public IActionResult GetByCustomerId(int customerId)
        {
            var orders = _service.GetAllOrders().Where(o => o.CustomerId == customerId);
            if (!orders.Any()) return NotFound();
            return Ok(orders);
        }

        // d. Delete the Order
        [HttpDelete("{id:int}")]
        public IActionResult Delete(int id)
        {
            var existing = _service.GetOrder(id);
            if (existing == null) return NotFound();

            _service.DeleteOrder(id);
            return NoContent();
        }

        [HttpPut("{id:int}")]
        public IActionResult Update(int id, OrderEntity order)
        {
            if (id != order.Id) return BadRequest("Wrong orderID");

            var existing = _service.GetOrder(id);
            if (existing == null) return NotFound();

            _service.UpdateOrder(order);
            return NoContent();
        }
    }
}
