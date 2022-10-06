using API.Services;
using Api.Database.Models;
using Api.Database.Repositories;
using FluentAssertions;
using API.Test.Mocks;
using API.Test.ObjectBuilders;
using Api.Database;

namespace API.Test
{
    public class CustomerServiceTest
    {
        private readonly ICustomerRepository _customerRepository;
        private readonly ApiContext _context = ApiContextMock.GetContext();
        private readonly CustomerService _sut;

        public CustomerServiceTest()
        {
            _customerRepository = new CustomerRepository(_context);
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

        [Fact]
        public async Task GetCustomerById_ValidId_ReturnsCustomer()
        {
            var customerId = 1;
            var customer = new CustomerBuilder().Build();

            _context.Customers.Add(customer);
            await _context.SaveChangesAsync();

            //var result = await _sut.GetCustomerById(customerId);

            //result.Id.Should().Be(customerId);
            //result.FirstName.Should().Be(customer.FirstName);
            //result.LastName.Should().Be(customer.LastName);
            //result.Email.Should().Be(result.Email);
        }

        [Fact]
        public async Task GetCustomerById_InvalidId_ThrowsException()
        {
            var customerId = -1;
            var customer = new CustomerBuilder().Build();

            _context.Customers.Add(customer);
            await _context.SaveChangesAsync();

            //await _sut.Invoking(sut => sut.GetCustomerById(customerId))
            //            .Should()
            //            .ThrowAsync<Exception>();
        }
    }
}