using Amazon;
using Amazon.Extensions.NETCore.Setup;
using Amazon.Runtime;
using Amazon.SQS;
using Cads.Cds.BuildingBlocks.Infrastructure.Messaging.Setup;
using Cads.Cds.BuildingBlocks.Testing.Support.Constants;
using FluentAssertions;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;

namespace Cads.Cds.BuildingBlocks.Infrastructure.Tests.Unit.Messaging.Setup;

public class ServiceCollectionExtensionsTests
{
    [Fact]
    public void AddAmazonSQSCore_Should_Register_Custom_SQS_Client_When_Localstack_Enabled()
    {
        // Arrange
        var services = new ServiceCollection();

        var configDict = new Dictionary<string, string?>
        {
            ["LOCALSTACK_ENDPOINT"] = TestAwsConstants.AwsServiceUrl,
            ["AWS:ServiceURL"] = TestAwsConstants.AwsServiceUrl,
            ["AWS:Region"] = TestAwsConstants.AwsRegion
        };

        IConfiguration config = new ConfigurationBuilder()
            .AddInMemoryCollection(configDict)
            .Build();

        // Act
        services.AddDefaultAWSOptions(new AWSOptions
        {
            Credentials = new BasicAWSCredentials("test", "test"),
            Region = RegionEndpoint.EUWest2
        });
        services.AddAmazonSQSCore(config);
        var provider = services.BuildServiceProvider();

        // Assert
        var sqs = provider.GetRequiredService<IAmazonSQS>();

        sqs.Should().BeOfType<AmazonSQSClient>();

        var client = (AmazonSQSClient)sqs;
        client.Config.ServiceURL.Should().Be(TestAwsConstants.AwsServiceUrl);
        client.Config.AuthenticationRegion.Should().Be(TestAwsConstants.AwsRegion);
        client.Config.UseHttp.Should().BeTrue();
    }

    [Fact]
    public void AddAmazonSQSCore_Should_Register_AwsService_When_Localstack_Disabled()
    {
        // Arrange
        var services = new ServiceCollection();

        IConfiguration config = new ConfigurationBuilder()
            .AddInMemoryCollection()
            .Build();

        // Act
        services.AddDefaultAWSOptions(new AWSOptions
        {
            Credentials = new BasicAWSCredentials("test", "test"),
            Region = RegionEndpoint.EUWest2
        });
        services.AddAmazonSQSCore(config);
        var provider = services.BuildServiceProvider();

        // Assert
        var sqs = provider.GetRequiredService<IAmazonSQS>();

        sqs.Should().BeOfType<AmazonSQSClient>();
    }

    [Fact]
    public void AddAmazonSQSCore_Should_Register_Singleton_When_Localstack_Enabled()
    {
        var services = new ServiceCollection();

        var config = new ConfigurationBuilder()
            .AddInMemoryCollection(new Dictionary<string, string?>
            {
                ["LOCALSTACK_ENDPOINT"] = "true",
                ["AWS:ServiceURL"] = TestAwsConstants.AwsServiceUrl,
                ["AWS:Region"] = TestAwsConstants.AwsRegion
            })
            .Build();

        services.AddDefaultAWSOptions(new AWSOptions
        {
            Credentials = new BasicAWSCredentials("test", "test"),
            Region = RegionEndpoint.EUWest2
        });
        services.AddAmazonSQSCore(config);

        var provider = services.BuildServiceProvider();

        var sqs1 = provider.GetRequiredService<IAmazonSQS>();
        var sqs2 = provider.GetRequiredService<IAmazonSQS>();

        sqs1.Should().BeSameAs(sqs2);
    }
}