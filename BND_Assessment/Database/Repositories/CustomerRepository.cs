using Database.Models;
using Microsoft.EntityFrameworkCore;

namespace Database.Repositories
{
    internal class CustomerRepository : ICustomerRepository
    {
        private readonly ApiContext _context;

        public CustomerRepository(ApiContext context)
        {
            _context = context;
        }

        public async Task<Customer> CreateCustomerAsync(Customer customerData)
        {
            _context.Customers.Add(customerData);
            await _context.SaveChangesAsync();

            return customerData;
        }

        public async Task<Customer> UpdateCustomerAsync(Customer customer)
        {
            _context.Customers.Update(customer);
            await _context.SaveChangesAsync();

            return customer;
        }

        public async Task<Customer> GetCustomerByIdAsync(int id)
        {
            return await _context.Customers.FirstOrDefaultAsync(x => x.Id == id);
        }

        public async Task<Customer> GetCustomerByAccountIdAsync(int id)
        {
            return await _context.Customers.FirstOrDefaultAsync(x => x.AccountId == id);
        }

        public async Task<IEnumerable<Customer>> GetAllCustomersAsync()
        {
            return await _context.Customers.ToListAsync();
        }
    }
}
