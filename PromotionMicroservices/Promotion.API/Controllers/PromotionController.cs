using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Promotion.ApplicationCore.Contracts.Services;
using Promotion.ApplicationCore.Entities;

namespace Promotion.API.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class PromotionController : ControllerBase
    {
        private readonly IPromotionService _services;
        public PromotionController(IPromotionService services)
        {
            _services = services;
        }

        [HttpGet]
        public ActionResult<IEnumerable<PromotionEntity>> Get()
        {
            return Ok(_services.GetList());
        }

        [HttpGet("{id:int}")]
        public ActionResult<PromotionEntity> GetById(int id)
        {
            var p = _services.GetById(id);
            return p is null ? NotFound() : Ok(p);
        }

        [HttpPost]
        public ActionResult<PromotionEntity> Save([FromBody] PromotionEntity promotion)
        {
            if (promotion is null) return BadRequest("Body required.");
            if (promotion.EndDate < promotion.StartDate)
                return BadRequest("EndDate cannot be before StartDate.");

            var created = _services.Save(promotion);
            return CreatedAtAction(nameof(GetById), new { id = created.Id }, created);
        }

        [HttpPut]
        public ActionResult<PromotionEntity> Update([FromBody] PromotionEntity promotion)
        {
            if (promotion is null || promotion.Id <= 0)
                return BadRequest("Valid Id is required.");
            if (promotion.EndDate < promotion.StartDate)
                return BadRequest("EndDate cannot be before StartDate.");

            return Ok(_services.Update(promotion));
        }

        [HttpDelete("delete-{id:int}")]
        public ActionResult<PromotionEntity> Delete(int id)
        {
            return Ok(_services.Delete(id));
        }

        [HttpGet("promotionByProductName")]
        public ActionResult<IEnumerable<PromotionEntity>> PromotionByProductName([FromQuery] string name)
        {
            return Ok(_services.GetByProductName(name));
        }

        [HttpGet("activePromotions")]
        public ActionResult<IEnumerable<PromotionEntity>> ActivePromotions([FromQuery] DateTime? asOf)
        {
            return Ok(_services.GetActive(asOf));
        }
    }
}
