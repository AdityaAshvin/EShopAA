using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Order.ApplicationCore.Contracts.Services;
using Order.ApplicationCore.Entities;

namespace Order.API.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class CustomerController : ControllerBase
    {
        private readonly ICustomerService _customerService;
        public CustomerController(ICustomerService customerService)
        {
            _customerService = customerService;
        }

        [HttpGet("GetCustomerAddressByUserId")]
        public IActionResult GetCustomerAddressByUserId(string userId)
        {
            if (string.IsNullOrEmpty(userId))
                return BadRequest("UserId null.");

            var addresses = _customerService.GetAddressesByUserId(userId);

            if (addresses == null || !addresses.Any())
                return NotFound($"No addresses found {userId}");

            return Ok(addresses);
        }

        [HttpPost("SaveCustomerAddress")]
        public IActionResult SaveCustomerAddress([FromBody] UserAddress address)
        {
            if (address == null)
                return BadRequest("Invalid address.");

            _customerService.SaveCustomerAddress(address);

            return Ok(address);
        }
    }
}
