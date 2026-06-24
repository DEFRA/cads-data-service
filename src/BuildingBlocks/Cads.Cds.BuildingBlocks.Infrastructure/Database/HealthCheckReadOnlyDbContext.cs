using Microsoft.EntityFrameworkCore;

namespace Cads.Cds.BuildingBlocks.Infrastructure.Database;

public class HealthCheckReadOnlyDbContext(DbContextOptions<HealthCheckReadOnlyDbContext> options) : CadsDbContext(options)
{
}