using Cads.Cds.BuildingBlocks.Infrastructure.Database.Abstractions;

namespace Cads.Cds.BuildingBlocks.Infrastructure.Database.Services;

public class PostgresStatusService(IEnumerable<HealthCheckDbContext> contexts) : IPostgresStatusService
{
    public async Task<bool> CanConnect(CancellationToken cancellationToken = default)
    {
        foreach (var healthCheckDbContext in contexts)
        {
            var canConnect = await healthCheckDbContext.Database.CanConnectAsync(cancellationToken);
            if (!canConnect) return false;
        }
        return true;
    }
}