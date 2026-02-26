using DotNet.Testcontainers.Builders;
using Xunit;

namespace Cads.Cds.BuildingBlocks.Testing.Support.TestFixtures.Containers;

using IContainer = DotNet.Testcontainers.Containers.IContainer;

// ReSharper disable once ClassNeverInstantiated.Global
public class ApiContainerFixture : IAsyncLifetime
{
    public IContainer ApiContainer { get; private set; } = null!;
    public HttpClient HttpClient { get; private set; } = null!;
    public PostgresFixture PostgresFixture { get; } = new();
    public LocalStackFixture LocalStackFixture { get; } = new();

    private const string NetworkName = "integration-test-network";

    public async ValueTask InitializeAsync()
    {
        await PostgresFixture.InitializeAsync();
        await LocalStackFixture.InitializeAsync();

        DockerNetworkHelper.EnsureNetworkExists(NetworkName);

        ApiContainer = new ContainerBuilder("cads_cds:latest")
          .WithName("cads_cds")
          .WithPortBinding(5555, 5555)
          .WithEnvironment("ASPNETCORE_ENVIRONMENT", "Development")
          .WithEnvironment("ASPNETCORE_HTTP_PORTS", "5555")
          .WithEnvironment("AWS__ServiceURL", LocalStackFixture.NetworkServiceUrl)
          .WithEnvironment("Modules__StorageBridge__Storage__CadsInternal__BucketName", LocalStackFixture.CadsInternalBucketName)
          .WithEnvironment("Modules__Ingester__Queues__CadsCds__QueueUrl", LocalStackFixture.CadsQueueUrl)
          .WithEnvironment("Modules__Ingester__Queues__CadsCds__DlqQueueUrl", LocalStackFixture.CadsDeadLetterQueueUrl)
          .WithEnvironment("LOCALSTACK_ENDPOINT", LocalStackFixture.NetworkServiceUrl)
          .WithEnvironment("Postgres__DefaultConnection", PostgresFixture.ConnectionString)
          .WithEnvironment("Postgres__ReadOnlyConnection", PostgresFixture.ReadConnectionString)
          .WithEnvironment("AWS_REGION", LocalStackFixture.AuthenticationRegion)
          .WithEnvironment("AWS_DEFAULT_REGION", LocalStackFixture.AuthenticationRegion)
          .WithEnvironment("AWS_ACCESS_KEY_ID", LocalStackFixture.AwsAccessKeyId)
          .WithEnvironment("AWS_SECRET_ACCESS_KEY", LocalStackFixture.AwsSecretAccessKey)
          .WithNetwork(NetworkName)
          .WithNetworkAliases("cads_cds")
          .WithWaitStrategy(Wait.ForUnixContainer()
              .UntilHttpRequestIsSucceeded(req => req.ForPort(5555).ForPath("/health"), o => o.WithTimeout(TimeSpan.FromSeconds(25))))
          .Build();

        await ApiContainer.StartAsync();

        HttpClient = new HttpClient { BaseAddress = new Uri($"http://localhost:{ApiContainer.GetMappedPublicPort(5555)}") };
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