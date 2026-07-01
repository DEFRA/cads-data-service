using Cads.Cds.StorageBridge.Infrastructure.Persistance.Contexts;
using Microsoft.EntityFrameworkCore;

namespace Cads.Cds.StorageBridge.Testing.Support.Contexts;

public class TestStorageBridgeReadDbContext(DbContextOptions<TestStorageBridgeReadDbContext> options)
    : StorageBridgeReadDbContext(options)
{
    /// <summary>
    /// Give fake keys so EF Core can track them (after base.OnModelCreating)
    /// </summary>
    /// <param name="modelBuilder"></param>
    protected override void OnModelCreating(ModelBuilder modelBuilder)
    {
        base.OnModelCreating(modelBuilder);
    }
}