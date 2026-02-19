using Amazon;
using Amazon.S3;
using Cads.Cds.BuildingBlocks.Infrastructure.Storage.Abstractions;
using Cads.Cds.BuildingBlocks.Infrastructure.Storage.Factories;
using Cads.Cds.BuildingBlocks.Infrastructure.Storage.Health;
using Microsoft.AspNetCore.Hosting;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Diagnostics.HealthChecks;

namespace Cads.Cds.BuildingBlocks.Infrastructure.Storage.Setup;

public static class ServiceCollectionExtensions
{
    public static IServiceCollection AddAmazonS3Core(this IServiceCollection services, IConfiguration configuration)
    {
        var amazonConfig = GetDefaultAmazonS3Config(configuration);
        services.AddSingleton(amazonConfig);

        services.AddSingleton<IS3ClientFactory, S3ClientFactory>();
        services.AddSingleton<IStartupFilter, ConfigureS3ClientsStartupFilter>();

        services.PostConfigure<HealthCheckServiceOptions>(options =>
        {
            var sp = services.BuildServiceProvider();
            var markers = sp.GetServices<IEnableS3HealthCheck>();

            if (markers.Any())
            {
                options.Registrations.Add(new HealthCheckRegistration(
                    "aws_s3",
                    sp => sp.GetRequiredService<AwsS3HealthCheck>(),
                    HealthStatus.Unhealthy,
                    ["aws", "s3"]
                ));
            }
        });
        services.AddTransient<AwsS3HealthCheck>();

        return services;
    }

    private static AmazonS3Config GetDefaultAmazonS3Config(IConfiguration configuration)
    {
        if (configuration["LOCALSTACK_ENDPOINT"] != null)
        {
            return new AmazonS3Config
            {
                ServiceURL = configuration["LOCALSTACK_ENDPOINT"],
                ForcePathStyle = true
            };
        }

        return new AmazonS3Config
        {
            RegionEndpoint = RegionEndpoint.EUWest2
        };
    }
}