using API.Database.Models;
using API.Database.Repositories;

namespace API.Services
{
    public class CustomerService : ICustomerService
    {
        private readonly ICustomerRepository _customerRepository;

        public CustomerService(ICustomerRepository customerRepository)
        {
            _customerRepository = customerRepository;
        }

        public async Task<Customer> CreateCustomer(Customer customer)
        {
            customer.CreatedDate = DateTime.Now;

            var createdCustomer = await _customerRepository.CreateCustomerAsync(customer);
            if (createdCustomer == null)
            {
                throw new Exception();
            }

            return createdCustomer;
        }

        public async Task<Customer> UpdateCustomer(Customer customer)
        {
            var updatedCustomer = await _customerRepository.UpdateCustomerAsync(customer);
            return updatedCustomer;
        }

        public async Task<Customer> GetCustomerById(int id)
        {
            var customer = await _customerRepository.GetCustomerByIdAsync(id);
            if (customer == null)
            {
                throw new Exception();
            }

            return customer;
        }

        public async Task<IEnumerable<Customer>> GetAllCustomers()
        {
            var customers = await _customerRepository.GetAllCustomersAsync();
            return customers;
        }
    }
}
