using Api.Database.Models;

namespace Api.Database.Repositories
{
    public interface ICustomerRepository
    {
        Task<Customer> CreateCustomerAsync(Customer customerData);
        Task<Customer> UpdateCustomerAsync(Customer customer);
        Task<Customer> GetCustomerByIdAsync(int id);
        Task<Customer> GetCustomerByAccountIdAsync(int id);
        Task<IEnumerable<Customer>> GetAllCustomersAsync();
    }
}
