using Cads.Cds.BuildingBlocks.Infrastructure.Database.Abstractions;
using Microsoft.Extensions.Diagnostics.HealthChecks;

namespace Cads.Cds.BuildingBlocks.Infrastructure.Database.Health;

public class PostgresHealthCheck(IPostgresStatusService postgresStatusService) : IHealthCheck
{
    public async Task<HealthCheckResult> CheckHealthAsync(HealthCheckContext context, CancellationToken cancellationToken = new CancellationToken())
    {
        var postgresStatusServiceResult = await postgresStatusService.CanConnect(cancellationToken);

        return new HealthCheckResult(
            status: postgresStatusServiceResult.CanConnect ? HealthStatus.Healthy : HealthStatus.Unhealthy,
            description: "Health check on Postgres database",
            data: postgresStatusServiceResult.CanConnect
                ? null
                : new Dictionary<string, object> { { "error", postgresStatusServiceResult.ErrorMessage } });
    }
}