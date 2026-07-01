using Cads.Cds.SystemAdmin.Infrastructure.Persistance.Contexts;
using Microsoft.EntityFrameworkCore;

namespace Cads.Cds.SystemAdmin.Testing.Support.Contexts;

public class TestSystemAdminReadDbContext(DbContextOptions<TestSystemAdminReadDbContext> options)
    : SystemAdminReadDbContext(options)
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