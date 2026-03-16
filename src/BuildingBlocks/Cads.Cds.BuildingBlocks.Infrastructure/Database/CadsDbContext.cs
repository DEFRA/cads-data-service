using Microsoft.EntityFrameworkCore;

namespace Cads.Cds.BuildingBlocks.Infrastructure.Database;

public class CadsDbContext(DbContextOptions<CadsDbContext> options) : BaseDbContext(options), ICadsDbContext
{
    protected override void ConfigureModel(ModelBuilder modelBuilder)
    {
    }
}