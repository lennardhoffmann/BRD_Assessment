using API.Database.Models;
using API.Models;

namespace API.Test.ObjectBuilders
{
    internal class TransactionBuilder
    {
        public Transaction Build()
        {
            return new Transaction
            {
                CustomerAccountId = 1,
                Description = "Test transaction",
                Amount = 100,
                TransactionDate = DateTime.Now
            };
        }
    }
}
