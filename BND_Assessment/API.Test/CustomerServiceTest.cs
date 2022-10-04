using API.Services;
using Database.Models;
using Database.Repositories;
using FluentAssertions;
using Xunit;

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

        [Fact]
        public async Task CreateCustomer_ValidData_ReturnsCreatedCustomerRecord()
        {
            var result = await _sut.CreateCustomer(new Customer());

            result.Should().NotBeNull();
        }
    }
}