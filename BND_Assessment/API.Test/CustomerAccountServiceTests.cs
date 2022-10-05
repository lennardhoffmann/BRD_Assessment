using Api.Database.Repositories;
using API.Services;
using API.Test.Mocks;

namespace API.Test
{
    public class CustomerAccountServiceTests
    {
        private readonly ICustomerAccountRepository _customerAccountRepository;
        private readonly CustomerAccountService _sut;

        public CustomerAccountServiceTests()
        {
            _customerAccountRepository = new CustomerAccountRepository(ApiContextMock.GetContext());
            _sut = new CustomerAccountService(_customerAccountRepository);
        }
    }
}
