using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Review.ApplicationCore.Contracts.Services;
using Review.ApplicationCore.Entities;

namespace Review.API.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class CustomerReviewController : ControllerBase
    {
        private readonly ICustomerReviewService _services;
        public CustomerReviewController(ICustomerReviewService services)
        {
            _services = services;
        }

        [HttpGet]
        public ActionResult<IEnumerable<CustomerReview>> Get()
        {
            return Ok(_services.GetList());
        }

        [HttpGet("{id:int}")]
        public ActionResult<CustomerReview> GetById(int id)
        {
            var r = _services.GetById(id);
            return r is null ? NotFound() : Ok(r);
        }

        [HttpGet("user/{userId:int}")]
        public ActionResult<IEnumerable<CustomerReview>> GetByUser(int userId)
        {
            return Ok(_services.GetByUserId(userId));
        }

        [HttpGet("product/{productId:int}")]
        public ActionResult<IEnumerable<CustomerReview>> GetByProduct(int productId)
        {
            return Ok(_services.GetByProductId(productId));
        }

        [HttpGet("year/{year:int}")]
        public ActionResult<IEnumerable<CustomerReview>> GetByYear(int year)
        {
            return Ok(_services.GetByYear(year));
        }

        [HttpPost]
        public ActionResult<CustomerReview> Save([FromBody] CustomerReview review)
        {
            if (review is null) return BadRequest("Body required.");
            
            if (review.ReviewDate == default) review.ReviewDate = DateTime.UtcNow;
            if (string.IsNullOrWhiteSpace(review.Status)) review.Status = "Pending";

            var created = _services.Save(review);
            return CreatedAtAction(nameof(GetById), new { id = created.Id }, created);
        }

        [HttpPut]
        public ActionResult<CustomerReview> Update([FromBody] CustomerReview review)
        {
            if (review is null || review.Id <= 0) return BadRequest("Valid Id required.");
            return Ok(_services.Update(review));
        }

        [HttpDelete("delete-{id:int}")]
        public ActionResult<CustomerReview> Delete(int id)
        {
            return Ok(_services.Delete(id));
        }

        [HttpPut("approve/{id:int}")]
        public ActionResult<CustomerReview> Approve(int id)
        {
            return Ok(_services.Approve(id));
        }

        [HttpPut("reject/{id:int}")]
        public ActionResult<CustomerReview> Reject(int id)
        {
            return Ok(_services.Reject(id));
        }
    }
}
