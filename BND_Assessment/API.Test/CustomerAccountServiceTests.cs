using API.Database;
using API.Database.Models;
using API.Database.Repositories;
using API.Models;
using API.Services;
using API.Test.Mocks;
using API.Test.ObjectBuilders;
using FluentAssertions;
using System.Text.RegularExpressions;

namespace API.Test
{
    public class CustomerAccountServiceTests
    {
        private const string _ibanPattern = "^NL\\d{2}[A-Z]{4}\\d{10}$";

        private readonly ApiContext _context = ApiContextMock.GetContext();
        private readonly ICustomerAccountRepository _customerAccountRepository;
        private readonly CustomerAccountService _sut;

        public CustomerAccountServiceTests()
        {
            _customerAccountRepository = new CustomerAccountRepository(_context);
            _sut = new CustomerAccountService(_customerAccountRepository);
        }

        [Fact]
        public async Task CreateCustomerAccount_WithValidParams_ReturnsCreatedCustomerAccount()
        {
            var accountData = new CustomerAccountBuilder()
                                    .Build();

            var result = await _sut.CreateCustomerAccount(accountData);

            result.Id.Should().Be(1);
            result.Balance.Should().Be(100);
        }

        [Fact]
        public async Task CreateCustomerAccount_WithValidParams_ShouldHaveValidIBAN()
        {
            var accountData = new CustomerAccountBuilder()
                                    .Build();

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
                        .ThrowAsync<BadRequestException>();
        }

        [Fact]
        public async Task GetCustomerAccount_WithValidId_ReturnsCustomerAccount()
        {
            var customerAccountId = 1;
            var accountData = new CustomerAccountBuilder()
                                    .Build();

            _context.CustomerAccounts.Add(accountData);
            await _context.SaveChangesAsync();

            var result = await _sut.GetCustomerAccountById(customerAccountId);

            result.Id.Should().Be(customerAccountId);
            result.Balance.Should().Be(100);
        }

        [Fact]
        public async Task GetCustomerAccount_InvalidId_ThrowsException()
        {
            var customerAccountId = -7;
            var accountData = new CustomerAccountBuilder()
                                    .Build();

            _context.CustomerAccounts.Add(accountData);
            await _context.SaveChangesAsync();

            await _sut.Invoking(sut => sut.GetCustomerAccountById(customerAccountId))
                        .Should()
                        .ThrowAsync<NotFoundException>();
        }

        [Fact]
        public async Task DepositAmount_ReturnsUpdatedCustomerAccount()
        {
            var depositDetails = new DepositDetails
            {
                DepositAmount = 50,
                CustomerAccountId = 1
            };

            var accountData = new CustomerAccountBuilder()
                                    .Build();

            var newBalance = accountData.Balance + (50 * 0.99999);

            _context.CustomerAccounts.Add(accountData);
            await _context.SaveChangesAsync();

            var result = await _sut.DepositAmount(depositDetails);

            result.Balance.Should().Be(newBalance);
        }
    }
}
