using Api.Database.Models;

namespace Api.Database.Repositories
{
    public interface ICustomerAccountRepository
    {
        Task<CustomerAccount> CreateCustomerAccountAsync(CustomerAccount accountData);
        Task<CustomerAccount> GetCustomerAccountByIdAsync(int id);
        Task<CustomerAccount> GetCustomerAccountByCustomerIdAsync(int id);
        Task<CustomerAccount> UpdateCustomerAccountAsync(CustomerAccount account);
        Task<IEnumerable<CustomerAccount>> GetAllCustomerAccountsAsync();
    }
}
