using API.Database;
using API.Database.Models;
using API.Database.Repositories;
using API.Services;
using API.Test.Mocks;
using API.Test.ObjectBuilders;
using FluentAssertions;

namespace API.Test
{
    public class ServiceChargeServiceTests
    {
        private readonly IServiceChargeRepository _serviceChargeRepository;
        private readonly ApiContext _context = ApiContextMock.GetContext();
        private readonly ServiceChargeService _sut;

        public ServiceChargeServiceTests()
        {
            _serviceChargeRepository = new ServiceChargeRepository(_context);
            _sut = new ServiceChargeService(_serviceChargeRepository);
        }

        [Fact]
        public async Task AddServiceCharge_ValidParams_ReturnsCreatedServcieCharge()
        {
            var serviceCharge = new ServiceChargeBuilder().Build();

            var result = await _sut.AddServiceCharge(serviceCharge);

            result.Id.Should().Be(1);
            result.Amount.Should().Be(serviceCharge.Amount);
        }

        [Fact]
        public async Task AddServiceCharge_InvalidParams_ThrowsException()
        {
            ServiceCharge serviceCharge = null;

            await _sut.Invoking(sut => sut.AddServiceCharge(serviceCharge))
                        .Should()
                        .ThrowAsync<Exception>();
        }
    }
}
