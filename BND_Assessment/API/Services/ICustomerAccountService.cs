using API.Database.Models;
using API.Models;

namespace API.Services
{
    public interface ICustomerAccountService
    {
        Task<CustomerAccount> CreateCustomerAccount(CustomerAccount accountData);
        Task<CustomerAccount> GetCustomerAccountById(int id);
        Task<IEnumerable<CustomerAccount>> GetAllCustomerAccounts();
        Task<CustomerAccount> GetCustomerAccountByCusytomerId(int customerId);
        Task<CustomerAccount> DepositAmount(DepositDetails depositDetails);
        Task<CustomerAccount> UpdateCustomerAccount(CustomerAccount customerAccount);
    }
}
