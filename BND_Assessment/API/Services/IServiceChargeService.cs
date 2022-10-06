using API.Database.Models;

namespace API.Services
{
    public interface IServiceChargeService
    {
       Task<ServiceCharge> AddServiceCharge(ServiceCharge serviceCharge);
    }
}
