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
          .WithEnvironment("StorageConfiguration__ComparisonReportsStorage__BucketName", "test-comparison-reports-bucket")
          .WithEnvironment("LOCALSTACK_ENDPOINT", "http://localstack:4566")
          .WithEnvironment("Postgres__DefaultConnection", PostgresFixture.ConnectionString)
          .WithEnvironment("Postgres__ReadOnlyConnection", PostgresFixture.ConnectionString)
          .WithNetwork(NetworkName)
          .WithNetworkAliases("cads_cds")
          .WithWaitStrategy(Wait.ForUnixContainer() /// for path health
              .UntilHttpRequestIsSucceeded(req => req.ForPort(5555).ForPath("/"), o => o.WithTimeout(TimeSpan.FromSeconds(25))))
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