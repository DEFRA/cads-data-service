using Microsoft.EntityFrameworkCore;

namespace Cads.Cds.BuildingBlocks.Infrastructure.Database;

public class HealthCheckDbContext(DbContextOptions<HealthCheckDbContext> options) : CadsDbContext(options)
{
}