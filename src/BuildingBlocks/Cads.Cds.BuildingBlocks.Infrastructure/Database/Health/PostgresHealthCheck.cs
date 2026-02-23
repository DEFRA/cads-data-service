using Cads.Cds.BuildingBlocks.Infrastructure.Database.Abstractions;
using Cads.Cds.BuildingBlocks.Infrastructure.Database.Services;
using Microsoft.Extensions.Diagnostics.HealthChecks;

namespace Cads.Cds.BuildingBlocks.Infrastructure.Database.Health;

public class PostgresHealthCheck(IPostgresStatusService postgresStatusService) : IHealthCheck
{
    public async Task<HealthCheckResult> CheckHealthAsync(HealthCheckContext context, CancellationToken cancellationToken = new CancellationToken())
    {
        var canConnect = await postgresStatusService.CanConnect(cancellationToken);

        return new HealthCheckResult(
            status: canConnect ? HealthStatus.Healthy : HealthStatus.Unhealthy,
            description: "Health check on Postgres database",
            data: canConnect
                ? null
                : new Dictionary<string, object> { { "error", "Could not connect to Postgres database" } });
    }
}