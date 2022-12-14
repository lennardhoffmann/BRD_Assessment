using API.Database.Models;
using Microsoft.EntityFrameworkCore;

namespace API.Database.Repositories
{
    public class CustomerAccountRepository : ICustomerAccountRepository
    {
        private readonly ApiContext _context;

        public CustomerAccountRepository(ApiContext context)
        {
            _context = context;
        }

        public async Task<CustomerAccount> CreateCustomerAccountAsync(CustomerAccount accountData)
        {
            _context.CustomerAccounts.Add(accountData);
            await _context.SaveChangesAsync();

            return accountData;
        }

        public async Task<CustomerAccount> GetCustomerAccountByIdAsync(int id)
        {
            return await _context.CustomerAccounts.FirstOrDefaultAsync(x => x.Id == id);
        }

        public async Task<CustomerAccount> UpdateCustomerAccountAsync(CustomerAccount account)
        {
            _context.CustomerAccounts.Update(account);
            await _context.SaveChangesAsync();

            return account;
        }
        public async Task<IEnumerable<CustomerAccount>> GetAllCustomerAccountsAsync()
        {
            return await _context.CustomerAccounts.ToListAsync();
        }

        public async Task<string> GetLastAccountNumberAsync()
        {
            var account = await _context.CustomerAccounts.LastOrDefaultAsync();
            if (account == null)
            {
                return string.Empty;
            }

            return account.AccountNumber;
        }
    }
}
