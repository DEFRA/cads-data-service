using Cads.Cds.MiBff.Core.Domain;
using Microsoft.EntityFrameworkCore;

namespace Cads.Cds.MiBff.Infrastructure.Persistence.Contexts;

public class MiBffWriteDbContext : DbContext
{
    public MiBffWriteDbContext(DbContextOptions<MiBffWriteDbContext> options)
        : base(options) { }

    public DbSet<MiUser> Users => Set<MiUser>();

    protected override void OnModelCreating(ModelBuilder modelBuilder)
    {
        modelBuilder.ApplyConfigurationsFromAssembly(typeof(MiBffWriteDbContext).Assembly);
    }
}