using API.Database.Models;

namespace API.Database.Repositories
{
    public class ServiceChargeRepository : IServiceChargeRepository
    {
        private readonly ApiContext _context;

        public ServiceChargeRepository(ApiContext context)
        {
            _context = context;
        }

        public async Task<ServiceCharge> AddServiceChargeAsync(ServiceCharge serviceCharge)
        {
            _context.Add(serviceCharge);
            await _context.SaveChangesAsync();

            return serviceCharge;
        }
    }
}
