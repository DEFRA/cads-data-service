using Cads.Cds.BuildingBlocks.Infrastructure.Configuration;
using Cads.Cds.BuildingBlocks.Infrastructure.Database.Health;
using Cads.Cds.BuildingBlocks.Infrastructure.Database.Services;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;

namespace Cads.Cds.BuildingBlocks.Infrastructure.Database.Setup;

public static class ServiceCollectionExtensions
{
    public static void ConfigureDatabase(this IServiceCollection services, IConfiguration configuration)
    {
        var postgresConfig = configuration
            .GetSection(PostgresConfiguration.SectionName)
            .Get<PostgresConfiguration>()
            ?? throw new InvalidOperationException(
                $"Configuration section '{PostgresConfiguration.SectionName}' is missing");

        services.AddSingleton(postgresConfig);

        if (string.IsNullOrWhiteSpace(postgresConfig.DefaultConnection))
        {
            throw new InvalidOperationException(
                "Connection string 'DefaultConnection' not found or empty");
        }

        services.AddPostgresDbContext<HealthCheckDbContext>(postgresConfig.DefaultConnection);

        services.AddScoped<PostgresHealthCheck>();
        services.AddScoped<IPostgresStatusService, PostgresStatusService>();
    }

    public static IServiceCollection AddPostgresDbContext<TContext>(this IServiceCollection services, string connectionString)
        where TContext : DbContext
    {
        services.AddDbContext<TContext>(options =>
            options.UseNpgsql(connectionString));

        return services;
    }
}