using API.Database.Models;

namespace API.Database.Repositories
{
    public interface IServiceChargeRepository
    {
        Task<ServiceCharge> AddServiceChargeAsync(ServiceCharge serviceCharge);
    }
}
