using Microsoft.EntityFrameworkCore;

namespace Cads.Cds.BuildingBlocks.Infrastructure.Database;

public class CadsReadOnlyDbContext(DbContextOptions<CadsReadOnlyDbContext> options) : BaseDbContext(options), ICadsDbContext
{
    protected override void ConfigureModel(ModelBuilder modelBuilder)
    {
    }
}