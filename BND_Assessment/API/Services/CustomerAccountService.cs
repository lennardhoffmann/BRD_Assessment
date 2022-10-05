using Api.Database.Repositories;

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

        private string GenerateIBAN()
        {
            var controlCode = new Random().Next(0, 100).ToString("D");
            var bank = "";
            //var accountNumber = new Random().Next(1000000000,9999999999);

            return $"{_countryIdentifier}{controlCode}";
        }
    }
}
