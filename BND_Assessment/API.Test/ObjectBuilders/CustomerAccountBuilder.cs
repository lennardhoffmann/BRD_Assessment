using API.Database.Models;

namespace API.Test.ObjectBuilders
{
    internal class CustomerAccountBuilder
    {
        private double _balance = 100;
        private string _iban = "NL82RABO9154010896";
        private string _accountNumber = "0000000001";

        public CustomerAccountBuilder WithBalance(double value)
        {
            _balance = value;
            return this;
        }

        public CustomerAccount Build()
        {
            return new CustomerAccount
            {
                Balance = _balance,
                AccountNumber = _accountNumber,
                IBAN = _iban,
                FirstName = "Lennard",
                LastName = "Testing",
                Email = "A@b.com",
            };
        }
    }
}
