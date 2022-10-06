using API.Database.Models;

namespace API.Test.ObjectBuilders
{
    internal class CustomerAccountBuilder
    {
        private int _customerId = 1;
        private double _balance = 100;
        private string _iban = "NL82RABO9154010896";

        public CustomerAccountBuilder WithBalance(double value)
        {
            _balance = value;
            return this;
        }

        public CustomerAccountBuilder WithCustomerId(int value)
        {
            _customerId = value;
            return this;
        }

        public CustomerAccount Build()
        {
            return new CustomerAccount
            {
                CustomerId = _customerId,
                Balance = _balance,
                IBAN = _iban
            };
        }
    }
}
