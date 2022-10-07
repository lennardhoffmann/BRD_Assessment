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
        private readonly ITransactionService _transactionService;

        public CustomerAccountsController(
            ICustomerAccountService customerAccountService,
            IServiceChargeService serviceChargeService,
            ITransactionService transactionService
            )
        {
            _customerAccountService = customerAccountService;
            _serviceChargeService = serviceChargeService;
            _transactionService = transactionService;
        }

        [HttpGet("getAccounts")]
        public async Task<IActionResult> Get()
        {
            var customerAccounts = await _customerAccountService.GetAllCustomerAccounts();
            return Ok(customerAccounts);
        }

        [HttpPost("createAccount")]
        public async Task<IActionResult> CreateAccount([FromBody] CustomerAccount customerAccount)
        {
            var createdAccount = await _customerAccountService.CreateCustomerAccount(customerAccount);

            return new ObjectResult(createdAccount) { StatusCode = StatusCodes.Status201Created };
        }

        [HttpGet("getAccount/{id}")]
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

            await _serviceChargeService.AddServiceCharge(serviceCharge);

            var transaction = new Transaction
            {
                Amount = (depositDetails.DepositAmount * 0.99999),
                CustomerAccountId = depositDetails.CustomerAccountId,
                Description = TransactionType.Deposit,
                CreatedDate = DateTime.Now
            };

            await _transactionService.AddTransaction(transaction);

            return Ok(updateResponse);
        }

        [HttpPut("transfer")]
        public async Task<IActionResult> TransferAmount([FromBody] InterCustomerTransfer transferDetails)
        {
            var sourceCustomerAccount = await _customerAccountService.GetCustomerAccountById(transferDetails.SourceCustomerAccountId);
            sourceCustomerAccount.Balance -= transferDetails.Amount;

            var updatedSourceCustomerAccount = await _customerAccountService.UpdateCustomerAccount(sourceCustomerAccount);
            var sourceTransaction = new Transaction
            {
                Amount = transferDetails.Amount,
                CustomerAccountId = transferDetails.SourceCustomerAccountId,
                Description = TransactionType.TranferSent,
                CreatedDate = DateTime.Now
            };

            await _transactionService.AddTransaction(sourceTransaction);

            var targetCustomerAccount = await _customerAccountService.GetCustomerAccountById(transferDetails.SourceCustomerAccountId);
            targetCustomerAccount.Balance += transferDetails.Amount;

            var updatedTargetCustomerAccount = await _customerAccountService.UpdateCustomerAccount(targetCustomerAccount);
            var targetTransaction = new Transaction
            {
                Amount = transferDetails.Amount,
                CustomerAccountId = transferDetails.DestinationCustomerAccountId,
                Description = TransactionType.TransferReceived
            };

            await _transactionService.AddTransaction(targetTransaction);

            return Ok(updatedSourceCustomerAccount != null && updatedTargetCustomerAccount != null);
        }
    }
}
