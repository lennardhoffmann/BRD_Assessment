namespace API.Models
{
    public class InterCustomerTransfer
    {
        public int SourceCustomerAccountId { get; set; }
        public int DestinationCustomerAccountId { get; set; }
        public double Amount { get; set; }
    }
}
