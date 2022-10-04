using API.Services;
using API.Test.Mocks;
using Database;
using Database.Repositories;

namespace API.Test
{
    public class CustomerServiceTest
    {
        private readonly ICustomerRepository _customerRepository;
        private readonly CustomerService _sut;

        public CustomerServiceTest()
        {
            _sut = new CustomerService(_customerRepository);
        }
    }
}