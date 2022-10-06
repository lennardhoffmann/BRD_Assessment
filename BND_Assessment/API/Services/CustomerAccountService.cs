using Api.Database.Models;
using Api.Database.Repositories;
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
            accountData.IBAN = GenerateIBAN();

            var createdAccount = await _customerAccountRepository.CreateCustomerAccountAsync(accountData);
            if (createdAccount == null)
            {
                throw new Exception("The customer account could not be created");
            }
            return createdAccount;
        }

        public async Task<CustomerAccount> GetCustomerAccountById(int id)
        {
            var customerAccount = await _customerAccountRepository.GetCustomerAccountByIdAsync(id);
            if (customerAccount == null)
            {
                throw new Exception();
            }

            return customerAccount;
        }

        public async Task<CustomerAccount> GetCustomerAccountByCusytomerId(int customerId)
        {
            var customerAccount = await _customerAccountRepository.GetCustomerAccountByCustomerIdAsync(customerId);
            if (customerAccount == null)
            {
                throw new Exception();
            }

            return customerAccount;
        }

        private string GenerateIBAN()
        {
            var controlCode = new Random().Next(0, 100).ToString("D");
            var bank = GetBankForIBAN();
            var accountNumber = new Random().NextInt64(1000000000, 9999999999).ToString("D10");

            return $"{_countryIdentifier}{controlCode}{bank}{accountNumber}";
        }

        private string GetBankForIBAN()
        {
            var bankValues = Enum.GetValues(typeof(Banks));
            var randomBank = (Banks)bankValues.GetValue(new Random().Next(bankValues.Length));

            return randomBank.ToString();
        }
    }
}
