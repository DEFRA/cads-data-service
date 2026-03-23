using Amazon.S3;
using Amazon.S3.Model;
using Amazon.SQS;
using Amazon.SQS.Model;
using Cads.Cds.BuildingBlocks.Infrastructure.Authentication.Configuration;
using Cads.Cds.BuildingBlocks.Infrastructure.Authentication.Handlers;
using Cads.Cds.BuildingBlocks.Infrastructure.Database.Abstractions;
using Cads.Cds.BuildingBlocks.Infrastructure.Database.Services;
using Cads.Cds.BuildingBlocks.Infrastructure.Persistence.Factories;
using Cads.Cds.BuildingBlocks.Infrastructure.Storage.Abstractions;
using Cads.Cds.BuildingBlocks.Infrastructure.Storage.Factories;
using Cads.Cds.BuildingBlocks.Testing.Support.Constants;
using Cads.Cds.BuildingBlocks.Testing.Support.Fakes.Authentication;
using Cads.Cds.MiBff.Infrastructure.Persistence.Contexts;
using Cads.Cds.StorageBridge.Infrastructure.Storage.Clients;
using Microsoft.AspNetCore.Authentication;
using Microsoft.AspNetCore.Authentication.JwtBearer;
using Microsoft.AspNetCore.Hosting;
using Microsoft.AspNetCore.Mvc.Testing;
using Microsoft.AspNetCore.TestHost;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.DependencyInjection.Extensions;
using Microsoft.Extensions.Hosting;
using Microsoft.Extensions.Options;
using Moq;
using System.Net;
using System.Text.Json;

namespace Cads.Cds.BuildingBlocks.Testing.Support.TestFixtures.Components;

public abstract class WebAppFactoryBase<TStart>(
    IDictionary<string, string?>? configOverrides = null,
    bool useFakeAuth = false) : WebApplicationFactory<TStart>
    where TStart : class

{
    public Mock<IAmazonSQS> AmazonSQSMock { get; private set; } = new();
    public Mock<IAmazonS3> AmazonS3Mock { get; private set; } = new();

    private readonly List<Action<IServiceCollection>> _serviceOverrides = [];
    private readonly IDictionary<string, string?> _configOverrides = configOverrides ?? new Dictionary<string, string?>();
    private readonly bool _useFakeAuth = useFakeAuth;

    protected override void ConfigureWebHost(IWebHostBuilder builder)
    {
        builder.UseSetting(WebHostDefaults.ApplicationKey, typeof(TStart).Assembly.FullName);
        builder.UseContentRoot(AppContext.BaseDirectory);
        builder.UseEnvironment("Test");

        // Configure server URLs for CoreWCF
        builder.UseUrls("http://localhost:5000", "https://localhost:5001");

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
            OverrideAmazonS3(services);
            ConfigureDatabase(services);

            foreach (var apply in _serviceOverrides)
                apply(services);

            services.RemoveAll<IHostedService>();
        });

        builder.ConfigureServices(services =>
        {
            var mockService = new Mock<IPostgresStatusService>();
            mockService.Setup(x => x.CanConnect(It.IsAny<CancellationToken>())).ReturnsAsync(new PostgresStatusServiceResult { CanConnect = true });

            services.RemoveAll<IPostgresStatusService>();
            services.AddScoped(x => mockService.Object);

            services.AddTransient<IStartupFilter, TestEndpointStartupFilter>();
        });
    }

    protected override IHost CreateHost(IHostBuilder builder)
    {
        if (_configOverrides.Count > 0)
        {
            builder.ConfigureAppConfiguration(config =>
            {
                config.AddInMemoryCollection(_configOverrides);
            });
        }

        return base.CreateHost(builder);
    }

    public T GetService<T>() where T : notnull
    {
        return Services.GetRequiredService<T>();
    }

    public void OverrideService(Action<IServiceCollection> action)
        => _serviceOverrides.Add(action);

    public void OverrideServiceAsSingleton<T>(T implementation) where T : class
    {
        _serviceOverrides.Add(services =>
        {
            services.RemoveAll<T>();
            services.AddSingleton(implementation);
        });
    }

    public void OverrideServiceAsTransient<T, TH>()
        where T : class
        where TH : class, T
    {
        _serviceOverrides.Add(services =>
        {
            services.RemoveAll<T>();
            services.AddTransient<T, TH>();
        });
    }

    public void OverrideServiceAsTransient<T>(T instance)
        where T : class
    {
        _serviceOverrides.Add(services =>
        {
            services.RemoveAll<T>();
            services.AddTransient(_ => instance);
        });
    }

    public void OverrideServiceAsScoped<T>(T implementation) where T : class
    {
        _serviceOverrides.Add(services =>
        {
            services.RemoveAll<T>();
            services.AddScoped(_ => implementation);
        });
    }

    public void ResetMocks()
    {
        ResetInfrastructureMocks();
    }

    private static void SetTestEnvironmentVariables()
    {
        Environment.SetEnvironmentVariable("AWS__ServiceURL", TestAwsConstants.AwsServiceUrl.TrimEnd('/'));
        Environment.SetEnvironmentVariable("Modules__Ingester__Queues__CadsCds__QueueUrl", TestSqsConstants.TestQueueUrl);
        Environment.SetEnvironmentVariable("Modules__Ingester__Queues__CadsCds__DlqQueueUrl", TestSqsConstants.TestQueueDlqUrl);
        Environment.SetEnvironmentVariable("Modules__StorageBridge__Storage__CadsInternal__BucketName", TestS3Constants.TestCadsInternalBucketName);

        Environment.SetEnvironmentVariable("AuthenticationConfiguration__ApiKey__Enabled", "true");
        Environment.SetEnvironmentVariable("AuthenticationConfiguration__Cognito__Enabled", "true");
        Environment.SetEnvironmentVariable("AuthenticationConfiguration__Cognito__Authority", TestAuthConstants.FakeCongnitoAuthority);
        Environment.SetEnvironmentVariable("AuthenticationConfiguration__AzureAD__Enabled", "true");
        Environment.SetEnvironmentVariable("AuthenticationConfiguration__AzureAD__Authority", TestAuthConstants.FakeAzureAdAuthority);
        Environment.SetEnvironmentVariable("AuthenticationConfiguration__AzureAD__Audience", TestAuthConstants.FakeAzureAdAudience);
    }

    private static void ConfigureFakeAuthorization(IServiceCollection services)
    {
        services.RemoveAll<IConfigureOptions<AuthenticationOptions>>();
        services.RemoveAll<IConfigureNamedOptions<JwtBearerOptions>>();

        services.RemoveAll<JwtBearerHandler>();
        services.RemoveAll<BasicAuthenticationHandler>();

        services.AddAuthentication()
            .AddScheme<AuthenticationSchemeOptions, FakeJwtHandler>(AuthenticationConstants.CognitoSchemeName, _ => { })
            .AddScheme<AuthenticationSchemeOptions, FakeJwtHandler>(AuthenticationConstants.AzureADSchemeName, _ => { })
            .AddScheme<AuthenticationSchemeOptions, FakeApiKeyHandler>(AuthenticationConstants.ApiKeySchemeName, _ => { });
    }

    private void ResetInfrastructureMocks()
    {
        AmazonSQSMock!.Reset();
        ApplyDefaultSqsMockSetup();

        AmazonS3Mock!.Reset();
        ApplyDefaultS3MockSetup();
    }

    private void OverrideAmazonSqs(IServiceCollection services)
    {
        services.RemoveAll<IAmazonSQS>();

        ApplyDefaultSqsMockSetup();

        services.AddSingleton(AmazonSQSMock.Object);
    }

    private void OverrideAmazonS3(IServiceCollection services)
    {
        services.RemoveAll<IAmazonS3>();

        ApplyDefaultS3MockSetup();

        services.AddSingleton(AmazonS3Mock.Object);

        services.RemoveAll<IS3ClientFactory>();

        services.AddSingleton<IS3ClientFactory>(sp =>
        {
            var factory = new S3ClientFactory();

            factory.RegisterMockClient<CadsInternalClient>(
                TestS3Constants.TestCadsInternalBucketName,
                AmazonS3Mock.Object);

            return factory;
        });
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

    private void ApplyDefaultS3MockSetup()
    {
        AmazonS3Mock
            .Setup(x => x.GetBucketAclAsync(It.IsAny<GetBucketAclRequest>(), It.IsAny<CancellationToken>()))
            .ReturnsAsync(new GetBucketAclResponse { HttpStatusCode = HttpStatusCode.OK });

        AmazonS3Mock
            .Setup(x => x.ListObjectsV2Async(It.IsAny<ListObjectsV2Request>(), It.IsAny<CancellationToken>()))
            .ReturnsAsync(new ListObjectsV2Response { HttpStatusCode = HttpStatusCode.OK });
    }

    protected virtual void ConfigureDatabase(IServiceCollection services)
    {
    }
}