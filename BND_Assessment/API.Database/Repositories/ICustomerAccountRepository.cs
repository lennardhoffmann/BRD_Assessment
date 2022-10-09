using API.Database.Models;

namespace API.Database.Repositories
{
    public interface ICustomerAccountRepository
    {
        Task<CustomerAccount> CreateCustomerAccountAsync(CustomerAccount accountData);
        Task<CustomerAccount> GetCustomerAccountByIdAsync(int id);
        Task<CustomerAccount> UpdateCustomerAccountAsync(CustomerAccount account);
        Task<IEnumerable<CustomerAccount>> GetAllCustomerAccountsAsync();
        Task<string> GetLastAccountNumberAsync();
    }
}
