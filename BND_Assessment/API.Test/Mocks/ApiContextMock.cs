using API.Test.Extensions;
using Database;
using Microsoft.EntityFrameworkCore;

namespace API.Test.Mocks
{
    public static class ApiContextMock
    {
        public static ApiContext GetContext(string? namedInstance = null)
        {
            var options = new DbContextOptionsBuilder<ApiContext>()
                .SetupInMemoryTestDatabase(namedInstance ?? Guid.NewGuid().ToString())
                .Options;

            return new ApiContext(options);
        }
    }
}
