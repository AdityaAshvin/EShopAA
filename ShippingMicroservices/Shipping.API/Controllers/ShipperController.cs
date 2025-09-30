using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Shipping.ApplicationCore.Contracts.Services;
using Shipping.ApplicationCore.Entities;

namespace Shipping.API.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class ShipperController : ControllerBase
    {
        private readonly IShipperService _services;
        public ShipperController(IShipperService services)
        {
            _services = services;
        }

        [HttpGet]
        public ActionResult<IEnumerable<Shipper>> Get()
        {
            return Ok(_services.GetList());
        }

        [HttpGet("{id:int}")]
        public ActionResult<Shipper> GetById(int id)
        {
            var s = _services.GetById(id);
            return s is null ? NotFound() : Ok(s);
        }

        [HttpPost]
        public ActionResult<Shipper> Save([FromBody] Shipper shipper)
        {
            if (shipper is null || string.IsNullOrWhiteSpace(shipper.Name))
                return BadRequest("Name is required.");
            var created = _services.Save(shipper);
            return CreatedAtAction(nameof(GetById), new { id = created.Id }, created);
        }

        [HttpPut]
        public ActionResult<Shipper> Update([FromBody] Shipper shipper)
        {
            if (shipper is null || shipper.Id <= 0)
                return BadRequest("Valid Id is required.");
            return Ok(_services.Update(shipper));
        }

        [HttpDelete("delete-{id:int}")]
        public ActionResult<Shipper> Delete(int id)
        {
            var deleted = _services.Delete(id);
            return Ok(deleted);
        }

        [HttpGet("region/{region}")]
        public ActionResult<IEnumerable<Shipper>> GetByRegion(string region)
        {
            return Ok(_services.GetByRegion(region));
        }

    }
}
