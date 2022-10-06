using API.Database.Models;
using API.Models;
using API.Services;
using Microsoft.AspNetCore.Mvc;

namespace API.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class CustomersController : ControllerBase
    {
        private readonly ICustomerService _customerService;
        private readonly ICustomerAccountService _customerAccountService;

        public CustomersController(
            ICustomerService customerService,
            ICustomerAccountService customerAccountService
            )
        {
            _customerService = customerService;
            _customerAccountService = customerAccountService;
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
        public async Task<IActionResult> CreateAccount([FromBody] DetailedCustomer customerData)
        {
            var customerCreateResponse = await _customerService.CreateCustomer(customerData.CustomerDetails);
            customerData.CustomerAccountDetails.CustomerId = customerCreateResponse.Id;

            var accountCreateResponse = await _customerAccountService.CreateCustomerAccount(customerData.CustomerAccountDetails);

            var createdData = new DetailedCustomer
            {
                CustomerDetails = customerCreateResponse,
                CustomerAccountDetails = accountCreateResponse
            };

            return new ObjectResult(createdData) { StatusCode = StatusCodes.Status201Created };
        }

        [HttpPut("customer")]
        public async Task<IActionResult> UpdateCustomer([FromBody] Customer customerData)
        {
            var updateResponse = await _customerService.UpdateCustomer(customerData);
            return Ok(updateResponse);
        }
    }
}
