using API.Database.Models;
using API.Models;
using API.Services;
using Microsoft.AspNetCore.Mvc;

namespace API.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class CustomerAccountsController : ControllerBase
    {
        private readonly ICustomerAccountService _customerAccountService;
        private readonly IServiceChargeService _serviceChargeService;

        public CustomerAccountsController(
            ICustomerAccountService customerAccountService,
            IServiceChargeService serviceChargeService
            )
        {
            _customerAccountService = customerAccountService;
            _serviceChargeService = serviceChargeService;
        }

        [HttpGet("customerAccounts")]
        public async Task<IActionResult> Get()
        {
            var customerAccounts = await _customerAccountService.GetAllCustomerAccounts();
            return Ok(customerAccounts);
        }

        [HttpPost("cstomerAccount")]
        public async Task<IActionResult> CreateAccount([FromBody] CustomerAccount customerAccount)
        {
            var createdAccount = await _customerAccountService.CreateCustomerAccount(customerAccount);

            return Ok();
        }

        [HttpGet("{id}")]
        public async Task<IActionResult> GetAccount(int id)
        {
            var customerAccount = await _customerAccountService.GetCustomerAccountById(id);
            return Ok(customerAccount);
        }

        [HttpPut("deposit")]
        public async Task<IActionResult> DepositAmount([FromBody] DepositDetails depositDetails)
        {
            var updateResponse = await _customerAccountService.DepositAmount(depositDetails);

            var serviceCharge = new ServiceCharge
            {
                Amount = (depositDetails.DepositAmount * 0.001),
                CustomerAccountReferenceId = depositDetails.CustomerAccountId,
                TransactionDate = DateTime.Now
            };

            var serviceChargeCreateResponse = await _serviceChargeService.AddServiceCharge(serviceCharge);

            return Ok(updateResponse);
        }

        [HttpPut("transfer")]
        public async Task<IActionResult> TransferAmount([FromBody] InterCustomerTransfer transferDetails)
        {
            var sourceCustomerAccount = await _customerAccountService.GetCustomerAccountById(transferDetails.SourceCustomerAccountId);
            sourceCustomerAccount.Balance -= transferDetails.Amount;

            var updatedSourceCustomerAccount = await _customerAccountService.UpdateCustomerAccount(sourceCustomerAccount);

            var targetCustomerAccount = await _customerAccountService.GetCustomerAccountById(transferDetails.SourceCustomerAccountId);
            targetCustomerAccount.Balance += transferDetails.Amount;

            var updatedTargetCustomerAccount = await _customerAccountService.UpdateCustomerAccount(targetCustomerAccount);

            return Ok(updatedSourceCustomerAccount != null && updatedTargetCustomerAccount != null);
        }
    }
}
