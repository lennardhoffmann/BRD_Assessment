using API.Database.Models;

namespace API.Services
{
    public interface ITransactionService
    {
        Task AddTransaction(Transaction transaction);
        Task<IEnumerable<Transaction>> GetAllTransactionsForCustomer(int id);
    }
}
