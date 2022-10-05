using Api.Database.Models;
using Api.Database.Repositories;
using API.Services;
using API.Test.Mocks;
using FluentAssertions;
using System.Text.RegularExpressions;

namespace API.Test
{
    public class CustomerAccountServiceTests
    {
        private const string _ibanPattern = "^NL\\d{2}[A-Z]{4}\\d{10}$";

        private readonly ICustomerAccountRepository _customerAccountRepository;
        private readonly CustomerAccountService _sut;

        public CustomerAccountServiceTests()
        {
            _customerAccountRepository = new CustomerAccountRepository(ApiContextMock.GetContext());
            _sut = new CustomerAccountService(_customerAccountRepository);
        }

        [Fact]
        public async Task CreateCustomerAccount_WithValidParams_ReturnsCreatedCustomerAccount()
        {
            var customerId = 1;
            var accountData = new CustomerAccount { CustomerId = customerId };

            var result = await _sut.CreateCustomerAccount(accountData);

            result.Id.Should().Be(1);
            result.CustomerId.Should().Be(customerId);
            result.Balance.Should().Be(0);
        }

        [Fact]
        public async Task CreateCustomerAccount_WithValidParams_ShouldHaveValidIBAN()
        {
            var customerId = 1;
            var accountData = new CustomerAccount { CustomerId = customerId };

            var result = await _sut.CreateCustomerAccount(accountData);

            var validIban = new Regex(@_ibanPattern).Match(result.IBAN).Success;

            validIban.Should().BeTrue();
        }

        [Fact]
        public async Task CreateCustomerAccount_InvalidCustomerData_ThrowsException()
        {
            CustomerAccount invalidCustomerAccount = null;

            await _sut.Invoking(sut => sut.CreateCustomerAccount(invalidCustomerAccount))
                        .Should()
                        .ThrowAsync<Exception>();
        }
    }
}
