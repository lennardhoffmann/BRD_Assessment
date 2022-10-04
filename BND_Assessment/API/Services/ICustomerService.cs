using Api.Database.Models;

namespace API.Services
{
    public interface ICustomerService
    {
        Task<Customer> CreateCustomer(Customer customer);
        Task<Customer> UpdateCustomer(Customer customer);
    }
}
