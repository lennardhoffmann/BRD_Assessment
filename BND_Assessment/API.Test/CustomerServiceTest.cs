using API.Services;
using Api.Database.Models;
using Api.Database.Repositories;
using FluentAssertions;
using API.Test.Mocks;
using API.Test.ObjectBuilders;

namespace API.Test
{
    public class CustomerServiceTest
    {
        private readonly ICustomerRepository _customerRepository;
        private readonly CustomerService _sut;

        public CustomerServiceTest()
        {
            _customerRepository = new CustomerRepository(ApiContextMock.GetContext());
            _sut = new CustomerService(_customerRepository);
        }

        [Fact]
        public async Task CreateCustomer_ValidCustomerData_ReturnsCreatedCustomerRecord()
        {
            var customer = new CustomerBuilder().Build();
            var result = await _sut.CreateCustomer(customer);

            result.Id.Should().Be(1);
            result.FirstName.Should().Be(customer.FirstName);
            result.LastName.Should().Be(customer.LastName);
            result.Email.Should().Be(result.Email);
        }

        [Fact]
        public async Task CreateCustomer_InvalidCustomerData_ThrowsException()
        {
            Customer invalidCustomer = null;

            await _sut.Invoking(sut => sut.CreateCustomer(invalidCustomer))
                        .Should()
                        .ThrowAsync<Exception>();
        }
    }
}