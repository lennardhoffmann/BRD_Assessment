using API.Database;
using API.Database.Models;

namespace API.Services
{
    public class DatabaseSeedingService
    {
        public static void SeedDatabase(WebApplication app)
        {
            var scope = app.Services.CreateScope();
            var database = scope.ServiceProvider.GetService<ApiContext>();

            SeedCustomers(database);
        }

        private async static void SeedCustomers(ApiContext database)
        {
            var customer1 = new CustomerAccount
            {
                FirstName = "Lennard",
                LastName = "Hoffmann",
                Email = "lennard@test.com",
                Balance = 250,
                AccountNumber = "0000000001",
                IBAN = "NL94INGB0000000001",
                CreatedDate = DateTime.Now
            };

            var customer2 = new CustomerAccount
            {
                FirstName = "Piet",
                LastName = "verdriet",
                Email = "peit@verdriet.com",
                Balance = 300,
                AccountNumber = "0000000002",
                IBAN = "NL92ABNA0000000002",
                CreatedDate = DateTime.Now
            };

            database.Add(customer1);
            database.Add(customer2);

            await database.SaveChangesAsync();
        }
    }
}
