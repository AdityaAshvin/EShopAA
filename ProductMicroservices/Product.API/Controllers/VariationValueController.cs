using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Product.ApplicationCore.Contracts.Services;
using Product.ApplicationCore.Entities;
using Product.ApplicationCore.Models;

namespace Product.API.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class VariationValueController : ControllerBase
    {
        private readonly IVariationValueService _services;
        public VariationValueController(IVariationValueService services)
        {
            _services = _services;
        }

        [HttpGet("GetVariationId")]
        public ActionResult<IEnumerable<VariationValue>> GetVariationId([FromQuery] int variationId)
        {
            var rows = _services.GetByVariationId(variationId);
            return Ok(rows);
        }

        [HttpPost("Save")]
        public ActionResult<VariationValue> Save([FromBody] VariationValueSaveRequest req)
        {
            if (req is null || req.VariationId <= 0 || string.IsNullOrWhiteSpace(req.Value))
                return BadRequest("variationId and value are required.");

            var created = _services.Save(req.VariationId, req.Value.Trim());
            return Ok(created);
        }

    }
}
