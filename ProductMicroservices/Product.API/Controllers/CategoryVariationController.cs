using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Product.ApplicationCore.Contracts.Services;
using Product.Infrastructure.Services;
using Product.ApplicationCore.Entities;

namespace Product.API.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class CategoryVariationController : ControllerBase
    {
        private readonly ICategoryVariationService _service;
        public CategoryVariationController(ICategoryVariationService service)
        {
            _service = service;
        }

        [HttpGet("GetCategoryVariationById", Name = "GetCategoryVariationById")]
        public ActionResult<CategoryVariation> GetCategoryVariationById([FromQuery] int id)
        {
            if (id <= 0) return BadRequest("id must be > 0");
            var v = _service.GetById(id);
            return v is null ? NotFound() : Ok(v);
        }

        [HttpPost("Save")]
        public ActionResult<CategoryVariation> Save([FromBody] CategoryVariation model)
        {
            if (!ModelState.IsValid) return ValidationProblem(ModelState);
            var saved = _service.Save(model);
            if (model.Id == 0)
                return CreatedAtAction(nameof(GetCategoryVariationById), new { id = saved.Id }, saved);
            return Ok(saved);
        }

        [HttpGet("GetAll")]
        public ActionResult<IEnumerable<CategoryVariation>> GetAll()
        {
            return Ok(_service.GetAll());
        }

        [HttpGet("GetCategoryVariationByCategoryId")]
        public ActionResult<IEnumerable<CategoryVariation>> GetByCategoryId([FromQuery] int categoryId)
        {
            if (categoryId <= 0) return BadRequest("categoryId must be > 0");
            return Ok(_service.GetByCategoryId(categoryId));
        }

        [HttpDelete("Delete")]
        public IActionResult Delete([FromQuery] int id)
        {
            if (id <= 0) return BadRequest("id must be > 0");
            var ok = _service.Delete(id);
            return ok ? NoContent() : Conflict("Cannot delete variation.");
        }

    }
}
