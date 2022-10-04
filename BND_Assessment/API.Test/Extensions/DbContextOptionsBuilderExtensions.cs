using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Diagnostics;

namespace API.Test.Extensions
{
    public static class DbContextOptionsBuilderExtensions
    {
        public static DbContextOptionsBuilder<T> SetupInMemoryTestDatabase<T>(this DbContextOptionsBuilder<T> builder, string namedInstance) where T : DbContext =>
            ((builder as DbContextOptionsBuilder).SetupInMemoryTestDatabase(namedInstance) as DbContextOptionsBuilder<T>)!;

        public static DbContextOptionsBuilder SetupInMemoryTestDatabase(this DbContextOptionsBuilder builder, string namedInstance) =>
            builder
                .UseInMemoryDatabase(namedInstance)
                .ConfigureWarnings(x => x.Ignore(InMemoryEventId.TransactionIgnoredWarning));
    }
}
