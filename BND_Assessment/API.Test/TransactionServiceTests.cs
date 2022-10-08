using API.Database.Repositories;
using API.Database;
using API.Services;
using API.Test.Mocks;
using API.Test.ObjectBuilders;
using FluentAssertions;
using Microsoft.EntityFrameworkCore;
using API.Database.Models;

namespace API.Test
{
    public class TransactionServiceTests
    {
        private readonly ITransactionRepository _transactionRepository;
        private readonly ApiContext _context = ApiContextMock.GetContext();
        private readonly TransactionService _sut;

        public TransactionServiceTests()
        {
            _transactionRepository = new TransactionRepository(_context);
            _sut = new TransactionService(_transactionRepository);
        }

        [Fact]
        public async Task AddTransaction_ValidParams_CreatesTransaction()
        {
            var transaction = new TransactionBuilder().Build();

            await _sut.AddTransaction(transaction);

            var result = await _context.TransactionHistory.FirstOrDefaultAsync();

            result.Id.Should().Be(1);
            result.CustomerAccountId.Should().Be(transaction.CustomerAccountId);
            result.Amount.Should().Be(transaction.Amount);
        }

        [Fact]
        public async Task AddTransaction_InvalidParams_ThrowsException()
        {
            Transaction transaction = null;

            await _sut.Invoking(sut => sut.AddTransaction(transaction))
                        .Should()
                        .ThrowAsync<Exception>();
        }

        [Fact]
        public async Task GetAllTransactionsForCustomer_ReturnsTransactions()
        {
            var customerAccountId = 1;
            var transaction = new TransactionBuilder().Build();

            _context.TransactionHistory.Add(transaction);
            await _context.SaveChangesAsync();

            var result = await _sut.GetAllTransactionsForCustomer(customerAccountId);
            result.Count().Should().Be(1);

            var record = result.FirstOrDefault();
            record.CustomerAccountId.Should().Be(customerAccountId);
        }
    }
}
