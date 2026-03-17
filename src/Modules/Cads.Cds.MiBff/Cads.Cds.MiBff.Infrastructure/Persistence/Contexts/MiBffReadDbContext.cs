using Cads.Cds.MiBff.Core.Domain;
using Microsoft.EntityFrameworkCore;

namespace Cads.Cds.MiBff.Infrastructure.Persistence.Contexts;

public class MiBffReadDbContext : DbContext
{
    public MiBffReadDbContext(DbContextOptions<MiBffReadDbContext> options)
        : base(options) { }

    public DbSet<MiUser> Users => Set<MiUser>();

    protected override void OnModelCreating(ModelBuilder modelBuilder)
    {
        modelBuilder.ApplyConfigurationsFromAssembly(typeof(MiBffReadDbContext).Assembly);
    }
}