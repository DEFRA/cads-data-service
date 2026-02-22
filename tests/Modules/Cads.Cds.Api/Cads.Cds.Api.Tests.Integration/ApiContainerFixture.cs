using DotNet.Testcontainers.Builders;

namespace Cads.Cds.Api.Tests.Integration;

using IContainer = DotNet.Testcontainers.Containers.IContainer;

// ReSharper disable once ClassNeverInstantiated.Global
public class ApiContainerFixture : IAsyncLifetime
{
    public IContainer ApiContainer { get; private set; } = null!;
    public HttpClient HttpClient { get; private set; } = null!;

    public string NetworkName { get; } = "integration-test-network";

    public async ValueTask InitializeAsync()
    {
        DockerNetworkHelper.EnsureNetworkExists(NetworkName);

        ApiContainer = new ContainerBuilder("cads_cds:latest")
          .WithName("cads_cds")
          .WithPortBinding(5555, 5555)
          .WithEnvironment("ASPNETCORE_ENVIRONMENT", "Development")
          .WithEnvironment("ASPNETCORE_HTTP_PORTS", "5555")
          .WithEnvironment("AWS__ServiceURL", "http://localstack:4566")
          .WithEnvironment("Modules__StorageBridge__Storage__CadsInternal__BucketName", LocalStackFixture.CadsInternalBucketName)
          .WithEnvironment("Modules__Ingester__Queues__CadsCds__QueueUrl", LocalStackFixture.CadsIntakeQueueUrl)
          .WithEnvironment("Modules__Ingester__Queues__CadsCds__DlqQueueUrl", LocalStackFixture.CadsDeadLetterQueueUrl)
          .WithEnvironment("LOCALSTACK_ENDPOINT", "http://localstack:4566")
          .WithEnvironment("Postgres__DefaultConnection", PostgresFixture.ConnectionString)
          .WithEnvironment("Postgres__ReadOnlyConnection", PostgresFixture.ConnectionString)
          .WithEnvironment("AWS_REGION", "eu-west-2")
          .WithEnvironment("AWS_DEFAULT_REGION", "eu-west-2")
          .WithEnvironment("AWS_ACCESS_KEY_ID", "test")
          .WithEnvironment("AWS_SECRET_ACCESS_KEY", "test")
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
        HttpClient?.Dispose();
        await ApiContainer.DisposeAsync();
    }
}