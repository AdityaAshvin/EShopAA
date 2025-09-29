using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Order.ApplicationCore.Contracts.Services;
using Order.ApplicationCore.Entities;

namespace Order.API.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class ShoppingCartController : ControllerBase
    {
        private readonly IShoppingCartService _cartService;
        public ShoppingCartController(IShoppingCartService cartService)
        {
            _cartService = cartService;

        }

        [HttpGet("GetShoppingCartByCustomerId")]
        public ActionResult<ShoppingCart> GetShoppingCartByCustomerId([FromQuery] int customerId)
        {
            if (customerId <= 0)
                return BadRequest("customerId invlalid.");

            var cart = _cartService.GetShoppingCartByCustomerId(customerId);
            if (cart == null)
                return NotFound();

            return Ok(cart);
        }

        [HttpPost("SaveShoppingCart")]
        public ActionResult<ShoppingCart> SaveShoppingCart([FromBody] ShoppingCart cart)
        {
            if (!ModelState.IsValid)
                return ValidationProblem(ModelState);

            var saved = _cartService.SaveShoppingCart(cart);
            return CreatedAtAction(nameof(GetShoppingCartByCustomerId),
                                   new { customerId = saved.CustomerId },
                                   saved);
        }

        [HttpDelete("DeleteShoppingCart/{id:int}")]
        public IActionResult DeleteShoppingCart([FromRoute] int id)
        {
            var deleted = _cartService.DeleteShoppingCart(id);
            if (!deleted)
                return NotFound();

            return NoContent();
        }
    }
}
