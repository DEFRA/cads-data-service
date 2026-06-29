using Cads.Cds.BuildingBlocks.Infrastructure.Database.Factories;
using Cads.Cds.BuildingBlocks.Infrastructure.Database.Setup;
using Cads.Cds.StorageBridge.Infrastructure.Persistance.Contexts;
using Cads.Cds.StorageBridge.Infrastructure.S3Import.Setup;
using Cads.Cds.StorageBridge.Infrastructure.Storage.Setup;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using Npgsql;
using OpenTelemetry.Metrics;
using OpenTelemetry.Resources;
using OpenTelemetry.Trace;
using System.Diagnostics.CodeAnalysis;

namespace Cads.Cds.StorageBridge.Infrastructure.Setup;

public static class ServiceCollectionExtensions
{
    public static IServiceCollection AddStorageBridgeInfrastructureLayer(this IServiceCollection services, IConfiguration config)
    {
        services.ConfigureStorageBridgePersistence();

        services.AddStorageBridgeStorage(config);

        services.ConfigureBulkLoadServices();

        services.ConfigurePrometheusScraping(config);

        return services;
    }

    [ExcludeFromCodeCoverage]
    private static void ConfigurePrometheusScraping(this IServiceCollection services, IConfiguration config)
    {
        var prometheusScrapingEnabled = config.GetValue<bool>("PrometheusScrapingEnabled");

        if (prometheusScrapingEnabled)
        {
            services.AddOpenTelemetry()
                .WithMetrics(metrics =>
                {
                    metrics
                        .SetResourceBuilder(ResourceBuilder.CreateDefault().AddService("CadsTelemetryApi"))
                        .AddMeter("Cads.Postgres.Metrics", "1.0") // Capture Npgsql metrics
                        .AddNpgsqlInstrumentation()
                        .AddConsoleExporter()
                        .AddPrometheusExporter(); // Expose metrics at /metrics
                }).WithTracing(tracing =>
                {
                    tracing
                        .SetResourceBuilder(ResourceBuilder.CreateDefault().AddService("CadsTelemetryApi"))
                        .AddNpgsql()
                        .AddConsoleExporter();
                });
        }
    }
}