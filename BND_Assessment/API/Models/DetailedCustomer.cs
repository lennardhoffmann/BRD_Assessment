using Api.Database.Models;

namespace API.Models
{
    public class DetailedCustomer
    {
        public Customer CustomerDetails { get; set; }
        public CustomerAccount CustomerAccountDetails { get; set; }
    }
}
