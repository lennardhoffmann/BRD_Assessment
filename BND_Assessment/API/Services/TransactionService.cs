using API.Database.Models;
using API.Database.Repositories;
using API.Models;

namespace API.Services
{
    public class TransactionService : ITransactionService
    {
        private readonly ITransactionRepository _transactionRepository;

        public TransactionService(ITransactionRepository transactionRepository)
        {
            _transactionRepository = transactionRepository;
        }

        public async Task AddTransaction(Transaction transaction)
        {
            var createdTransaction = await _transactionRepository.AddTransactionAsync(transaction);
            if (createdTransaction == null)
            {
                throw new BadRequestException($"Transacton could not be created for CustomerAccount with Id {transaction.CustomerAccountId}");
            }
        }

        public async Task<IEnumerable<Transaction>> GetAllTransactionsForCustomer(int id)
        {
            return await _transactionRepository.GetAllTransactionsForCustomerAsync(id);
        }
    }
}
