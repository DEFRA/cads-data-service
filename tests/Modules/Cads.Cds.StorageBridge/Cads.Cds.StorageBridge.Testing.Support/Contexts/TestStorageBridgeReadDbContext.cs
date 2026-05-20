using Cads.Cds.StorageBridge.Infrastructure.Persistance.Contexts;
using Microsoft.EntityFrameworkCore;

namespace Cads.Cds.StorageBridge.Testing.Support.Contexts;

public class TestStorageBridgeReadDbContext(DbContextOptions<StorageBridgeReadDbContext> options)
    : StorageBridgeReadDbContext(options)
{
    protected override void OnModelCreating(ModelBuilder modelBuilder)
    {
        base.OnModelCreating(modelBuilder);
    }
}
