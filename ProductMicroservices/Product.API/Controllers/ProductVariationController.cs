using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Product.ApplicationCore.Contracts.Services;
using Product.ApplicationCore.Models;

namespace Product.API.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class ProductVariationController : ControllerBase
    {
        private readonly IProductVariationService _services;
        public ProductVariationController(IProductVariationService services)
        {
            _services = services;
        }

        [HttpGet("GetProductVariation")]
        public ActionResult<IEnumerable<ProductVariationResponse>> GetProductVariation([FromQuery] int productId)
        {
            return Ok(_services.Get(productId));
        }

        [HttpPost("Save")]
        public IActionResult Save([FromBody] ProductVariationSaveRequest req)
        {
            if (req == null || req.ProductId <= 0)
                return BadRequest("Invalid request");
            _services.Save(req.ProductId, req.VariationValueIds ?? new List<int>());
            return NoContent();
        }
    }
}
