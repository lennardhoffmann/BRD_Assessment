namespace API.Database.Models
{
    public class ServiceCharge
    {
        public int Id { get; set; }
        public int CustomerAccountReferenceId { get; set; }
        public double Amount { get; set; }
        public DateTime TransactionDate { get; set; }
    }
}
