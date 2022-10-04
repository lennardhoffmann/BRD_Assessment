using Api.Database.Repositories;

namespace API.Services
{
    public class CustomerAccountService : ICustomerAccountService
    {
        private readonly ICustomerAccountRepository _customerAccountRepository;

        public CustomerAccountService(ICustomerAccountRepository customerAccountRepository)
        {
            _customerAccountRepository = customerAccountRepository;
        }
    }
}
