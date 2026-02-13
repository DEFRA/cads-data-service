using Cads.Cds.BuildingBlocks.Infrastructure.Configuration.Aws;
using System.Text.Json.Serialization;
using Cads.Cds.BuildingBlocks.Infrastructure;
using Microsoft.Extensions.Diagnostics.HealthChecks;

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
        builder.Add(new HealthCheckRegistration("postgreshealth", (x) => x.GetService<PostgresService>()!, null, null));
    }
}