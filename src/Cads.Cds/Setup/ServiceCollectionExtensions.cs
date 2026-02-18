using Cads.Cds.BuildingBlocks.Infrastructure.Configuration.Aws;
using Cads.Cds.BuildingBlocks.Infrastructure.Database.Health;
using Microsoft.Extensions.Diagnostics.HealthChecks;
using System.Text.Json.Serialization;

namespace Cads.Cds.Setup;

public static class ServiceCollectionExtensions
{
    public static void ConfigureCds(this IServiceCollection services, IConfiguration configuration)
    {
        services.AddControllers()
            .AddJsonOptions(opts =>
            {
                var enumConverter = new JsonStringEnumConverter();
                opts.JsonSerializerOptions.Converters.Add(enumConverter);
            });

        services.AddDefaultAWSOptions(configuration.GetAWSOptions());
        services.Configure<AwsConfig>(configuration.GetSection(AwsConfig.SectionName));

        services.ConfigureHealthChecks();

        services.AddModules(configuration);
    }

    private static void ConfigureHealthChecks(this IServiceCollection services)
    {
        var builder = services.AddHealthChecks();
        builder.Add(new HealthCheckRegistration(
            name: "postgres",
            factory: (x) => x.GetRequiredService<PostgresHealthCheck>(),
            failureStatus: HealthStatus.Unhealthy,
            tags: ["db", "postgres"]));
    }
}