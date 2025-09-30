using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Product.ApplicationCore.Contracts.Services;
using Product.ApplicationCore.Entities;

namespace Product.API.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class ProductController : ControllerBase
    {
        private readonly IProductService _services;
        public ProductController(IProductService services)
        {
            _services = services;
        }

        [HttpGet("GetListProducts")]
        public ActionResult<IEnumerable<ProductEntity>> GetListProducts()
        {
            return Ok(_services.GetList());
        }

        [HttpGet("GetProductById")]
        public ActionResult<ProductEntity> GetProductById([FromQuery] int id)
        {
            var p = _services.GetById(id);
            return p is null ? NotFound() : Ok(p);
        }

        [HttpPost("Save")]
        public ActionResult<ProductEntity> Save([FromBody] ProductEntity product)
        {
            var created = _services.Save(product);
            return CreatedAtAction(nameof(GetProductById), new { id = created.Id }, created);
        }

        [HttpPut("Update")]
        public ActionResult<ProductEntity> Update([FromBody] ProductEntity product)
        {
            if (product.Id <= 0) return BadRequest("Id invalid");
            return Ok(_services.Update(product));
        }

        [HttpPut("InActive")]
        public ActionResult<ProductEntity> InActive([FromQuery] int id)
        {
            return Ok(_services.InActive(id));
        }

        [HttpGet("GetProductByCategoryId")]
        public ActionResult<IEnumerable<ProductEntity>> GetByCategoryId([FromQuery] int categoryId)
        {
           return Ok(_services.GetByCategoryId(categoryId));
        }

        [HttpGet("GetProductByName")]
        public ActionResult<IEnumerable<ProductEntity>> GetByName([FromQuery] string name) =>
            Ok(_services.GetByName(name));

        [HttpDelete("DeleteProduct")]
        public ActionResult<ProductEntity> Delete([FromQuery] int id)
        {
            return Ok(_services.Delete(id));
        }
    }
}
