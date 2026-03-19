using Microsoft.EntityFrameworkCore;

namespace Cads.Cds.MiBff.Infrastructure.Tests.Unit.Repositories
{
    internal class TestDbContextHelper
    {
        public static T CreateInMemoryDbContext<T>(string dbName)
            where T : DbContext
        {
            var options = new DbContextOptionsBuilder<T>()
                .UseInMemoryDatabase(databaseName: dbName) // Unique name per test to isolate data
                .Options;

            var instance = Activator.CreateInstance(typeof(T), options);
            if (instance is T ctx)
            {
                return ctx;
            }

            throw new InvalidOperationException(
                $"Could not create an instance of {typeof(T)}. " +
                "Ensure the type has a constructor accepting DbContextOptions or DbContextOptions<T>."
            );
        }
    }
}