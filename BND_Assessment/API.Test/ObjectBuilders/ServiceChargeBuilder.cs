using API.Database.Models;

namespace API.Test.ObjectBuilders
{
    internal class ServiceChargeBuilder
    {
        private double _amount = 10;
        private int _customerAccountReferenceId = 1;

        public ServiceCharge Build()
        {
            return new ServiceCharge
            {
                TransactionDate = DateTime.Now,
                Amount = _amount,
                CustomerAccountReferenceId = _customerAccountReferenceId,
            };
        }
    }
}
