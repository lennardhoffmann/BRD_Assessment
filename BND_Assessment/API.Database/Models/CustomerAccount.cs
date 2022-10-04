namespace Api.Database.Models
{
    public class CustomerAccount
    {
        public int Id { get; set; }
        public int CustomerId { get; set; }
        public string IBAN { get; set; }
        public double Balance { get; set; }
        public DateTime CreatedDate { get; set; }
        public DateTime ModifiedDate { get; set; }
    }
}
