using Microsoft.Extensions.Diagnostics.HealthChecks;

namespace Cads.Cds.BuildingBlocks.Infrastructure;

public class PostgresHealthCheckService : IHealthCheck
{
    private readonly ApplicationDbContext _context;

    public PostgresHealthCheckService(ApplicationDbContext context)
    {
        _context = context;
    }

    public async Task<HealthCheckResult> CheckHealthAsync(HealthCheckContext context, CancellationToken cancellationToken = new CancellationToken())
    {
        var canConnect = await _context.Database.CanConnectAsync(cancellationToken);

        return new HealthCheckResult(canConnect ? HealthStatus.Healthy : HealthStatus.Unhealthy);
    }
}