using Microsoft.EntityFrameworkCore;

namespace Cads.Cds.StorageBridge.Testing.Support.Seeding;

public static class TestStorageBridgeDataSeeder
{
    /// <summary>
    /// Add in an order that respects FK constraints
    /// </summary>
    /// <param name="context"></param>
    public static void Seed(DbContext context)
    {

    }

    public static void SeedSaveChanges(DbContext context)
    {
        context.SaveChanges();
    }
}