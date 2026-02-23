using Cads.Cds.BuildingBlocks.Infrastructure.Database.Abstractions;

namespace Cads.Cds.BuildingBlocks.Infrastructure.Database.Services;

public class PostgresStatusService(HealthCheckDbContext healthCheckDbContext, HealthCheckReadOnlyDbContext healthCheckReadOnlyDbContext) : IPostgresStatusService
{ 
    public async Task<PostgresStatusServiceResult> CanConnect(CancellationToken cancellationToken = default)
    {
        var canConnectTask = healthCheckDbContext.Database.CanConnectAsync(cancellationToken);
        var readOnlyCanConnectTask = healthCheckReadOnlyDbContext.Database.CanConnectAsync(cancellationToken);
    
        var results = await Task.WhenAll(canConnectTask, readOnlyCanConnectTask);
        var result = new PostgresStatusServiceResult { CanConnect = results[0] && results[1] };
        if(!results[0]) result.ErrorMessage = "Could not connect to default Postgres database.";
        if(!results[1]) result.ErrorMessage += "Could not connect to readonly Postgres database.";
        return result;
    }
}