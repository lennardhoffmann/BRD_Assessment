using API.Database.Models;
using Microsoft.EntityFrameworkCore;

namespace API.Database
{
    public class ApiContext : DbContext
    {
        public ApiContext(DbContextOptions<ApiContext> options) : base(options)
        {
        }

        public DbSet<CustomerAccount> CustomerAccounts { get; set; }
        public DbSet<ServiceCharge> ServiceCharges { get; set; }
        public DbSet<Transaction> TransactionHistory { get; set; }
    }
}
