using Cads.Cds.BuildingBlocks.Infrastructure.Authentication.Configuration;
using Cads.Cds.BuildingBlocks.Testing.Support.Constants;
using Cads.Cds.BuildingBlocks.Testing.Support.Utilities.Authorization;
using DotNet.Testcontainers.Builders;
using DotNet.Testcontainers.Configurations;
using Xunit;

namespace Cads.Cds.BuildingBlocks.Testing.Support.TestFixtures.Containers;

using IContainer = DotNet.Testcontainers.Containers.IContainer;

// ReSharper disable once ClassNeverInstantiated.Global
public class ApiContainerFixture : IAsyncLifetime
{
    public IContainer ApiContainer { get; private set; } = null!;
    public HttpClient HttpClient { get; private set; } = null!;
    public HttpClient HttpsClient { get; private set; } = null!;
    public PostgresFixture PostgresFixture { get; } = new();
    public LocalStackFixture LocalStackFixture { get; } = new();

    public async ValueTask InitializeAsync()
    {
        await PostgresFixture.InitializeAsync();
        await LocalStackFixture.InitializeAsync();

        DockerNetworkHelper.EnsureNetworkExists(TestContainerConstants.NetworkName);

        var certPath = Path.Combine(AppContext.BaseDirectory, "certs");

        ApiContainer = new ContainerBuilder("cads_cds:latest")
          .WithEnvironment("ASPNETCORE_ENVIRONMENT", "Test")
          .WithEnvironment("ASPNETCORE_URLS", "http://0.0.0.0:5555;https://0.0.0.0:5556")
          .WithPortBinding(5555, true)
          .WithPortBinding(5556, true)
          .WithBindMount(certPath, "/https", AccessMode.ReadOnly)
          .WithEnvironment("Kestrel__Certificates__Default__Path", "/https/https-dev-cert.pfx")
          .WithEnvironment("Kestrel__Certificates__Default__Password", "testpassword")
          .WithEnvironment("AWS__ServiceURL", LocalStackFixture.NetworkServiceUrl)
          .WithEnvironment("Modules__StorageBridge__Storage__CadsInternal__BucketName", LocalStackFixture.CadsInternalBucketName)
          .WithEnvironment("Modules__Ingester__Queues__CadsCds__QueueUrl", LocalStackFixture.CadsQueueUrl)
          .WithEnvironment("Modules__Ingester__Queues__CadsCds__DlqQueueUrl", LocalStackFixture.CadsDeadLetterQueueUrl)
          .WithEnvironment("AuthenticationConfiguration__ApiKey__Enabled", "true")
          .WithEnvironment("LOCALSTACK_ENDPOINT", LocalStackFixture.NetworkServiceUrl)
          .WithEnvironment("Postgres__DefaultConnection", PostgresFixture.ConnectionString)
          .WithEnvironment("Postgres__ReadOnlyConnection", PostgresFixture.ReadConnectionString)
          .WithEnvironment("AWS_REGION", LocalStackFixture.AuthenticationRegion)
          .WithEnvironment("AWS_DEFAULT_REGION", LocalStackFixture.AuthenticationRegion)
          .WithEnvironment("AWS_ACCESS_KEY_ID", LocalStackFixture.AwsAccessKeyId)
          .WithEnvironment("AWS_SECRET_ACCESS_KEY", LocalStackFixture.AwsSecretAccessKey)
          .WithEnvironment("DOTNET_SYSTEM_NET_SOCKETS_HTTP_USEIPV6", "false")
          .WithNetwork(TestContainerConstants.NetworkName)
          .WithNetworkAliases("cads_cds")
          .WithWaitStrategy(Wait.ForUnixContainer()
              .UntilHttpRequestIsSucceeded(req => req.ForPort(5555).ForPath("/health"), o => o.WithTimeout(TimeSpan.FromSeconds(25))))
          .Build();

        await ApiContainer.StartAsync();

        HttpClient = new HttpClient { BaseAddress = new Uri($"http://localhost:{ApiContainer.GetMappedPublicPort(5555)}") };
        HttpClient.AddBasicApiKey(TestAuthConstants.BasicApiKey, TestAuthConstants.BasicSecret);

        var handler = new HttpClientHandler
        {
            ServerCertificateCustomValidationCallback = (sender, cert, chain, errors) => true
        };
        HttpsClient = new HttpClient(handler) { BaseAddress = new Uri($"https://localhost:{ApiContainer.GetMappedPublicPort(5556)}") };
        HttpsClient.AddBasicApiKey(TestAuthConstants.BasicApiKey, TestAuthConstants.BasicSecret);
    }

    public async ValueTask DisposeAsync()
    {
        Exception? error = null;

        async ValueTask Safe(Func<ValueTask> f)
        {
            try { await f(); }
            catch (Exception ex) { error ??= ex; }
        }

        await Safe(() => PostgresFixture.DisposeAsync());
        await Safe(() => LocalStackFixture.DisposeAsync());

        try { HttpClient?.Dispose(); }
        catch (Exception ex) { error ??= ex; }

        await Safe(() => ApiContainer.DisposeAsync());

        GC.SuppressFinalize(this);

        if (error is not null)
            throw error;
    }
}