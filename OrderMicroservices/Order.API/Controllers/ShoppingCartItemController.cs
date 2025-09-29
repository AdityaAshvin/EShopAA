using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Order.ApplicationCore.Contracts.Services;

namespace Order.API.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class ShoppingCartItemController : ControllerBase
    {
        private readonly IShoppingCartItemService _service;

        public ShoppingCartItemController(IShoppingCartItemService service)
        {
            _service = service;
        }

        [HttpDelete("DeleteShoppingCartItemById")]
        public IActionResult DeleteShoppingCartItemById([FromQuery] int id)
        {
            if (id <= 0) return BadRequest("id must be greater than zero.");

            var ok = _service.DeleteShoppingCartItemById(id);
            return ok ? NoContent() : NotFound();
        }
    }
}
