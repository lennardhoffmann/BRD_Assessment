using API.Services;
using API.Test.Mocks;
using Database;
using Database.Repositories;

namespace API.Test
{
    public class CustomerAccountServiceTests
    {
        private readonly ICustomerAccountRepository _customerAccountRepository;
        private readonly ApiContext _apiContext = ApiContextMock.GetContext();

        private readonly CustomerAccountService _sut;

        public CustomerAccountServiceTests()
        {
            _sut = new CustomerAccountService(_customerAccountRepository);
        }
    }
}
