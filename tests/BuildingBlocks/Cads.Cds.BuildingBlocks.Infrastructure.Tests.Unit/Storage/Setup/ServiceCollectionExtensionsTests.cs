using Amazon;
using Amazon.S3;
using Cads.Cds.BuildingBlocks.Infrastructure.Storage.Abstractions;
using Cads.Cds.BuildingBlocks.Infrastructure.Storage.Factories;
using Cads.Cds.BuildingBlocks.Infrastructure.Storage.Health;
using Cads.Cds.BuildingBlocks.Infrastructure.Storage.Setup;
using Cads.Cds.BuildingBlocks.Testing.Support.Constants;
using Microsoft.AspNetCore.Hosting;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Diagnostics.HealthChecks;

namespace Cads.Cds.BuildingBlocks.Infrastructure.Tests.Unit.Storage.Setup;

public class ServiceCollectionExtensionsTests
{
    private static ServiceProvider BuildProvider(Action<IServiceCollection>? configure = null, IDictionary<string, string>? config = null)
    {
        var services = new ServiceCollection();

        var configuration = new ConfigurationBuilder()
            .AddInMemoryCollection(
                (config ?? new Dictionary<string, string>())
                    .ToDictionary(kvp => kvp.Key, kvp => (string?)kvp.Value)
            )
            .Build();

        services.AddLogging();
        services.AddAmazonS3Core(configuration);

        configure?.Invoke(services);

        return services.BuildServiceProvider();
    }

    [Fact]
    public void Registers_AmazonS3Config_With_Localstack_Endpoint()
    {
        var provider = BuildProvider(
            config: new Dictionary<string, string>
            {
                ["LOCALSTACK_ENDPOINT"] = TestAwsConstants.AwsServiceUrl
            });

        var cfg = provider.GetRequiredService<AmazonS3Config>();

        Assert.Equal(TestAwsConstants.AwsServiceUrl, cfg.ServiceURL);
        Assert.True(cfg.ForcePathStyle);
    }

    [Fact]
    public void Registers_AmazonS3Config_With_Default_Region_When_No_Localstack()
    {
        var provider = BuildProvider();

        var cfg = provider.GetRequiredService<AmazonS3Config>();

        Assert.Equal(RegionEndpoint.EUWest2, cfg.RegionEndpoint);
        Assert.Null(cfg.ServiceURL);
    }

    [Fact]
    public void Registers_Core_Services()
    {
        var provider = BuildProvider();

        Assert.IsType<S3ClientFactory>(provider.GetRequiredService<IS3ClientFactory>());
        Assert.IsType<ConfigureS3ClientsStartupFilter>(provider.GetRequiredService<IStartupFilter>());
        Assert.IsType<AwsS3HealthCheck>(provider.GetRequiredService<AwsS3HealthCheck>());
    }

    [Fact]
    public void Does_Not_Register_S3_HealthCheck_When_No_Module_Enables_It()
    {
        var provider = BuildProvider();

        var options = provider.GetRequiredService<Microsoft.Extensions.Options.IOptions<HealthCheckServiceOptions>>().Value;

        Assert.DoesNotContain(options.Registrations, r => r.Name == "aws_s3");
    }

    [Fact]
    public void Registers_S3_HealthCheck_When_Module_Enables_It()
    {
        var provider = BuildProvider(services =>
        {
            services.AddSingleton<IEnableS3HealthCheck, TestS3HealthCheckMarker>();
        });

        var options = provider.GetRequiredService<Microsoft.Extensions.Options.IOptions<HealthCheckServiceOptions>>().Value;

        Assert.Contains(options.Registrations, r => r.Name == "aws_s3");
    }

    private class TestS3HealthCheckMarker : IEnableS3HealthCheck { }
}