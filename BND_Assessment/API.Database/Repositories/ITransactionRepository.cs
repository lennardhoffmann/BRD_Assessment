using API.Database.Models;

namespace API.Database.Repositories
{
    public interface ITransactionRepository
    {
        Task<Transaction> AddTransactionAsync(Transaction transaction);
        Task<IEnumerable<Transaction>> GetAllTransactionsForCustomerAsync(int customerAccountId);
    }
}
