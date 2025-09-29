using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Product.ApplicationCore.Contracts.Services;
using Product.ApplicationCore.Entities;
using Product.Infrastructure.Services;

namespace Product.API.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class CategoryController : ControllerBase
    {
        private readonly IProductCategoryService _service;
        public CategoryController(IProductCategoryService service)
        {
            _service = service;
        }

        [HttpPost("SaveCategory")]
        public ActionResult<ProductCategory> SaveCategory([FromBody] ProductCategory model)
        {
            if (!ModelState.IsValid) return ValidationProblem(ModelState);
            var saved = _service.SaveCategory(model);
            if (model.Id == 0)
                return CreatedAtAction(nameof(GetCategoryById), new { id = saved.Id }, saved);
            return Ok(saved);
        }

        [HttpGet("GetAllCategory")]
        public ActionResult<IEnumerable<ProductCategory>> GetAllCategory()
        {
            return Ok(_service.GetAllCategory());
        }

        [HttpGet("GetCategoryById")]
        public ActionResult<ProductCategory> GetCategoryById([FromQuery] int id)
        {
            if (id <= 0) return BadRequest("id must be > 0");
            var cat = _service.GetCategoryById(id);
            return cat is null ? NotFound() : Ok(cat);
        }

        [HttpDelete("Delete")]
        public IActionResult Delete([FromQuery] int id)
        {
            if (id <= 0) return BadRequest("id must be > 0");
            var ok = _service.Delete(id);
            return ok ? NoContent() : Conflict("Cannot delete category that has subcategories or products.");
        }

        [HttpGet("GetCategoryByParentCategoryId")]
        public ActionResult<IEnumerable<ProductCategory>> GetByParent([FromQuery] int? parentCategoryId)
        {
            return Ok(_service.GetCategoryByParentCategoryId(parentCategoryId));
        }

    }
}
