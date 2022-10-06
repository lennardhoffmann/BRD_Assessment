using API.Database.Models;
using API.Services;
using Microsoft.AspNetCore.Mvc;

namespace API.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class CustomersController : ControllerBase
    {
        private readonly ICustomerService _customerService;

        public CustomersController(ICustomerService customerService)
        {
            _customerService = customerService;
        }

        // GET: api/<CustomersController>
        [HttpGet("customers")]
        public async Task<IActionResult> GetAllCustomers()
        {
            var customers = await _customerService.GetAllCustomers();
            return Ok(customers);
        }

        [HttpGet("customer/{id}")]
        public async Task<IActionResult> GetCustomer(int id)
        {
            var customer = await _customerService.GetCustomerById(id);
            return Ok(customer);
        }

        [HttpPost("customer")]
        public async Task<IActionResult> Post([FromBody] Customer customerData)
        {
            var response = await _customerService.CreateCustomer(customerData);
            return new ObjectResult(response) { StatusCode = StatusCodes.Status201Created };
        }

        [HttpPut("customer")]
        public async Task<IActionResult> UpdateCustomer([FromBody] Customer customerData)
        {
            var updateResponse = await _customerService.UpdateCustomer(customerData);
            return Ok(updateResponse);
        }
    }
}
