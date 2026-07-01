using Cads.Cds.SystemAdmin.Infrastructure.Persistance.Contexts;
using Microsoft.EntityFrameworkCore;

namespace Cads.Cds.SystemAdmin.Testing.Support.Contexts;

public class TestSystemAdminReadDbContext(DbContextOptions<TestSystemAdminReadDbContext> options)
    : SystemAdminReadDbContext(options)
{
    protected override void OnModelCreating(ModelBuilder modelBuilder)
    {
        base.OnModelCreating(modelBuilder);
    }
}