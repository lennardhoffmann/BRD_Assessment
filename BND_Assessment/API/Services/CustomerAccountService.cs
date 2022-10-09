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
            accountData.AccountNumber = await GenerateAccountNumber();
            accountData.IBAN = GenerateIBAN(accountData.AccountNumber);

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

            customerAccount.Balance += Math.Round((depositDetails.DepositAmount * 0.999),2);

            var updateResponse = await _customerAccountRepository.UpdateCustomerAccountAsync(customerAccount);

            return updateResponse;
        }

        public async Task<CustomerAccount> UpdateCustomerAccount(CustomerAccount customerAccount)
        {
            customerAccount.ModifiedDate = DateTime.Now;

            var updateResponse = await _customerAccountRepository.UpdateCustomerAccountAsync(customerAccount);
            if (updateResponse == null)
            {
                throw new BadRequestException($"Could not update customer account with Id {customerAccount.Id}");
            }

            return updateResponse;
        }

        private async Task<string> GenerateAccountNumber()
        {
            var lastAccountNumber = await _customerAccountRepository.GetLastAccountNumberAsync();

            if (string.IsNullOrWhiteSpace(lastAccountNumber) || lastAccountNumber == "9999999999")
            {
                lastAccountNumber = "0000000000";
            }

            var newAccountNumber = Int64.Parse(lastAccountNumber) + 1;
            return newAccountNumber.ToString("D10");
        }

        private static string GenerateIBAN(string accountNumber)
        {
            var controlCode = new Random().Next(0, 100).ToString("D");
            var bank = GetBankForIBAN();

            return $"{_countryIdentifier}{controlCode}{bank}{accountNumber}";
        }

        private static string GetBankForIBAN()
        {
            var bankValues = Enum.GetValues(typeof(Banks));
            var randomBank = (Banks)bankValues.GetValue(new Random().Next(bankValues.Length));

            return randomBank.ToString();
        }
    }
}
