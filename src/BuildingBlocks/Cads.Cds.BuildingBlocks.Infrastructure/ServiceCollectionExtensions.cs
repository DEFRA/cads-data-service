using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Diagnostics.HealthChecks;
using Microsoft.Extensions.Options;

namespace Cads.Cds.BuildingBlocks.Infrastructure;

public static class ServiceCollectionExtensions
{
    public static void ConfigureBuildingBlocks(this IServiceCollection services, IConfiguration configuration)
    {
        services.Configure<PostgresConfiguration>(configuration.GetSection(PostgresConfiguration.SectionName));
        services.AddSingleton<PostgresService>();
    }
}

public class PostgresConfiguration
{
    public static readonly string SectionName = "Postgres";
    public string DefaultConnection { get; set; }
    public string ReadOnlyConnection { get; set; }
}

public class PostgresService : IHealthCheck
{
    private readonly IOptions<PostgresConfiguration> _postgresConfig;

    public PostgresService(IOptions<PostgresConfiguration> postgresConfig)
    {
        _postgresConfig = postgresConfig;
    }
    
    public bool CheckIsHealthy()
    {
        return !string.IsNullOrEmpty(_postgresConfig?.Value.DefaultConnection);
    }

    public Task<HealthCheckResult> CheckHealthAsync(HealthCheckContext context, CancellationToken cancellationToken = new CancellationToken())
    {
        return Task.FromResult(new HealthCheckResult(CheckIsHealthy() ? HealthStatus.Healthy : HealthStatus.Unhealthy));
    }
}