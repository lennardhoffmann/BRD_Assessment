using Api.Database.Models;

namespace API.Services
{
    public interface ICustomerAccountService
    {
        Task<CustomerAccount> CreateCustomerAccount(CustomerAccount accountData);
    }
}
