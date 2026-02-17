using Amazon.SQS;
using Amazon.SQS.Model;
using Cads.Cds.BuildingBlocks.Testing.Support.Constants;
using Cads.Cds.BuildingBlocks.Infrastructure;
using Microsoft.AspNetCore.Hosting;
using Microsoft.AspNetCore.Mvc.Testing;
using Microsoft.AspNetCore.TestHost;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.DependencyInjection.Extensions;
using Microsoft.Extensions.Hosting;
using Moq;
using System.Net;
using Cads.Cds.BuildingBlocks.Infrastructure.Database.Services;

namespace Cads.Cds.BuildingBlocks.Testing.Support.TestFixtures;

public abstract class WebAppFactoryBase<TStart>(
    IDictionary<string, string?>? configOverrides = null,
    bool useFakeAuth = false) : WebApplicationFactory<TStart>
    where TStart : class

{
    public Mock<IAmazonSQS> AmazonSQSMock { get; private set; } = new();

    private readonly List<Action<IServiceCollection>> _serviceOverrides = [];
    private readonly IDictionary<string, string?> _configOverrides = configOverrides ?? new Dictionary<string, string?>();
    private readonly bool _useFakeAuth = useFakeAuth;

    protected override void ConfigureWebHost(IWebHostBuilder builder)
    {
        builder.UseSetting(WebHostDefaults.ApplicationKey, typeof(TStart).Assembly.FullName);

        SetTestEnvironmentVariables();

        builder.ConfigureAppConfiguration((context, configBuilder) =>
        {
            if (_configOverrides.Count > 0)
                configBuilder.AddInMemoryCollection(_configOverrides);
        });

        builder.ConfigureTestServices(services =>
        {
            if (_useFakeAuth)
            {
                ConfigureFakeAuthorization(services);
            }

            OverrideAmazonSqs(services);

            foreach (var apply in _serviceOverrides)
                apply(services);

            services.RemoveAll<IHostedService>();
        });

        builder.ConfigureServices(services =>
        {
            var mockService = new Mock<IPostgresStatusService>();
            mockService.Setup(x => x.CanConnect(It.IsAny<CancellationToken>())).ReturnsAsync(true);
            services.RemoveAll<IPostgresStatusService>();
            services.AddScoped<IPostgresStatusService>(x => mockService.Object);
        });
    }

    public void OverrideService(Action<IServiceCollection> action)
        => _serviceOverrides.Add(action);

    public void ResetMocks()
    {
        ResetInfrastructureMocks();
    }

    private static void SetTestEnvironmentVariables()
    {
        Environment.SetEnvironmentVariable("AWS__ServiceURL", TestAwsConstants.AwsServiceUrl.TrimEnd('/'));
        Environment.SetEnvironmentVariable("Modules__Ingester__Queues__CadsCds__QueueUrl", TestSqsConstants.TestQueueUrl);
        Environment.SetEnvironmentVariable("Modules__Ingester__Queues__CadsCds__DlqQueueUrl", TestSqsConstants.TestQueueDlqUrl);
    }

    private static void ConfigureFakeAuthorization(IServiceCollection services)
    {
    }

    private void ResetInfrastructureMocks()
    {
        AmazonSQSMock!.Reset();
        ApplyDefaultSqsMockSetup();
    }

    private void OverrideAmazonSqs(IServiceCollection services)
    {
        services.RemoveAll<IAmazonSQS>();

        ApplyDefaultSqsMockSetup();

        services.AddSingleton(AmazonSQSMock.Object);
    }

    private void ApplyDefaultSqsMockSetup()
    {
        AmazonSQSMock
            .Setup(x => x.GetQueueAttributesAsync(
                It.IsAny<string>(),
                It.IsAny<List<string>>(),
                It.IsAny<CancellationToken>()))
            .ReturnsAsync(new GetQueueAttributesResponse
            {
                HttpStatusCode = HttpStatusCode.OK
            });

        AmazonSQSMock
            .Setup(x => x.GetQueueAttributesAsync(
                It.IsAny<GetQueueAttributesRequest>(),
                It.IsAny<CancellationToken>()))
            .Throws(new NotImplementedException("Use the (string, List<string>) overload"));
    }
}