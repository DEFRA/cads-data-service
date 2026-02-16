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
        bool canConnect = false;
        Exception? exception = null;
        try
        {
            canConnect = await _context.Database.CanConnectAsync(cancellationToken);
        }
        catch (Exception e)
        {
            exception = e;
        }

        var data = new Dictionary<string, object>();
        
        if (exception != null)
        {
            data["error"] = $"{exception.Message} - {exception.InnerException?.Message}";
        }

        return new HealthCheckResult(
            status: canConnect ? HealthStatus.Healthy : HealthStatus.Unhealthy,
            description: $"Could not connect to postgres.",
            exception: exception,
            data: data);
    }
}