using API.Database.Models;
using API.Database.Repositories;
using API.Models;

namespace API.Services
{
    public class CustomerAccountService : ICustomerAccountService
    {
        private const string _countryIdentifier = "NL";
        private readonly ICustomerAccountRepository _customerAccountRepository;

        public CustomerAccountService(ICustomerAccountRepository customerAccountRepository)
        {
            _customerAccountRepository = customerAccountRepository;
        }

        public async Task<CustomerAccount> CreateCustomerAccount(CustomerAccount accountData)
        {
            accountData.CreatedDate = DateTime.Now;
            accountData.IBAN = GenerateIBAN();

            var createdAccount = await _customerAccountRepository.CreateCustomerAccountAsync(accountData);
            if (createdAccount == null)
            {
                throw new BadRequestException("The customer account could not be created");
            }

            return createdAccount;
        }

        public async Task<CustomerAccount> GetCustomerAccountById(int id)
        {
            var customerAccount = await _customerAccountRepository.GetCustomerAccountByIdAsync(id);
            if (customerAccount == null)
            {
                throw new NotFoundException($"Could not retrieve CustomerAccount with Id {id}");
            }

            return customerAccount;
        }

        public async Task<IEnumerable<CustomerAccount>> GetAllCustomerAccounts()
        {
            var customerAccounts = await _customerAccountRepository.GetAllCustomerAccountsAsync();
            return customerAccounts;
        }

        public async Task<CustomerAccount> DepositAmount(DepositDetails depositDetails)
        {
            var customerAccount = await _customerAccountRepository.GetCustomerAccountByIdAsync(depositDetails.CustomerAccountId);
            if (customerAccount == null)
            {
                throw new NotFoundException($"Could not retrieve CustomerAccount with Id {depositDetails.CustomerAccountId}");
            }

            customerAccount.Balance += (depositDetails.DepositAmount * 0.999);

            var updateResponse = await _customerAccountRepository.UpdateCustomerAccountAsync(customerAccount);

            return updateResponse;
        }

        public async Task<CustomerAccount> UpdateCustomerAccount(CustomerAccount customerAccount)
        {
            var updateResponse = await _customerAccountRepository.UpdateCustomerAccountAsync(customerAccount);
            if (updateResponse == null)
            {
                throw new BadRequestException($"Could not update customer account with Id {customerAccount.Id}");
            }

            return updateResponse;
        }

        private static string GenerateIBAN()
        {
            var controlCode = new Random().Next(0, 100).ToString("D");
            var bank = GetBankForIBAN();
            var accountNumber = new Random().NextInt64(1000000000, 9999999999).ToString("D10");

            return $"{_countryIdentifier}{controlCode}{bank}{accountNumber}";
        }

        private static string GenerateAccountNumber()
        {
            return "";
        }

        private static string GetBankForIBAN()
        {
            var bankValues = Enum.GetValues(typeof(Banks));
            var randomBank = (Banks)bankValues.GetValue(new Random().Next(bankValues.Length));

            return randomBank.ToString();
        }
    }
}
