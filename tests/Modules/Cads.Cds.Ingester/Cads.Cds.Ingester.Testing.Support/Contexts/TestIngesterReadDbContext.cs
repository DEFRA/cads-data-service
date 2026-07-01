using Cads.Cds.Ingester.Infrastructure.Persistence.Contexts;
using Microsoft.EntityFrameworkCore;

namespace Cads.Cds.Ingester.Testing.Support.Contexts;

public class TestIngesterReadDbContext(DbContextOptions<TestIngesterReadDbContext> options)
    : IngesterReadDbContext(options)
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