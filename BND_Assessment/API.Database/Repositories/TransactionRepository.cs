using API.Database.Models;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace API.Database.Repositories
{
    public class TransactionRepository : ITransactionRepository
    {
        private readonly ApiContext _context;

        public TransactionRepository(ApiContext context)
        {
            _context = context;
        }

        public async Task<Transaction> AddTransactionAsync(Transaction transaction)
        {
            _context.Add(transaction);
            await _context.SaveChangesAsync();

            return transaction;
        }

        public async Task<IEnumerable<Transaction>> GetAllTransactionsForCustomerAsync(int customerAccountId)
        {
            return await _context.TransactionHistory.Where(x => x.CustomerAccountId == customerAccountId).ToListAsync();
        }
    }
}
