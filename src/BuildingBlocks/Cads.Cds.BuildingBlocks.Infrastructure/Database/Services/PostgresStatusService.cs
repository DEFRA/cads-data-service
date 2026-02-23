using Cads.Cds.BuildingBlocks.Infrastructure.Database.Abstractions;

namespace Cads.Cds.BuildingBlocks.Infrastructure.Database.Services;

public class PostgresStatusService(HealthCheckDbContext context) : IPostgresStatusService
{
    private readonly HealthCheckDbContext _context = context;

    public Task<bool> CanConnect(CancellationToken cancellationToken = default)
    {
        return _context.Database.CanConnectAsync(cancellationToken);
    }
}