using API.Database.Models;

namespace API.Test.ObjectBuilders
{
    public class CustomerBuilder
    {
        private readonly Customer _customer = new();

        public CustomerBuilder()
        {
            _customer.FirstName = "Lennard";
            _customer.LastName = "Testing";
            _customer.Email = "A@b.com";
        }

        public Customer Build()
        {
            return _customer;
        }
    }
}
