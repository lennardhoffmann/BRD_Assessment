using API.Database.Models;

namespace API.Test.ObjectBuilders
{
    internal class TransactionBuilder
    {
        public Transaction Build()
        {
            return new Transaction
            {
                CustomerAccountId = 1,
                Description = "",
                Amount = 100,
                CreatedDate = DateTime.Now
            };
        }
    }
}
