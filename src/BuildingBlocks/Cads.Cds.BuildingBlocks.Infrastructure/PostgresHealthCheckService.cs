using Microsoft.Extensions.Diagnostics.HealthChecks;

namespace Cads.Cds.BuildingBlocks.Infrastructure;

public class PostgresHealthCheckService : IHealthCheck
{
    private readonly IPostgresStatusService _postgresStatusService;

    public PostgresHealthCheckService(IPostgresStatusService postgresStatusService)
    {
        _postgresStatusService = postgresStatusService;
    }

    public async Task<HealthCheckResult> CheckHealthAsync(HealthCheckContext context, CancellationToken cancellationToken = new CancellationToken())
    {
        var canConnect = await _postgresStatusService.CanConnect(cancellationToken);

        return new HealthCheckResult(
            status: canConnect ? HealthStatus.Healthy : HealthStatus.Unhealthy,
            description: $"Could not connect to postgres.");
    }
}