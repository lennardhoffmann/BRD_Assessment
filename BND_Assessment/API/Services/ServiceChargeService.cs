using API.Database.Models;
using API.Database.Repositories;

namespace API.Services
{
    public class ServiceChargeService : IServiceChargeService
    {
        private readonly IServiceChargeRepository _serviceChargeRepository;

        public ServiceChargeService(IServiceChargeRepository serviceChargeRepository)
        {
            _serviceChargeRepository = serviceChargeRepository;
        }

        public async Task AddServiceCharge(ServiceCharge serviceCharge)
        {
            var createdServiceCharge = await _serviceChargeRepository.AddServiceChargeAsync(serviceCharge);
            if (createdServiceCharge == null)
            {
                throw new Exception();
            }
        }
    }
}
