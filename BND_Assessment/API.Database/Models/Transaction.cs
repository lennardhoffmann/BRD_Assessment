namespace API.Database.Models
{
    public class Transaction
    {
        public int Id { get; set; }
        public int CustomerAccountId { get; set; }
        public string Description { get; set; }
        public double Amount { get; set; }
        public DateTime CreatedDate { get; set; }
    }
}
