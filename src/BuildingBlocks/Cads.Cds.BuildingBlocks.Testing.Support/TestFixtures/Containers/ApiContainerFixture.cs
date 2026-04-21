using Cads.Cds.BuildingBlocks.Testing.Support.Constants;
using Cads.Cds.BuildingBlocks.Testing.Support.Fakes.Authentication;
using Cads.Cds.BuildingBlocks.Testing.Support.TestFixtures.Containers.Configuration;
using Cads.Cds.BuildingBlocks.Testing.Support.Utilities.Authorization;
using DotNet.Testcontainers.Builders;
using DotNet.Testcontainers.Configurations;
using System.Net.Http.Headers;
using Xunit;

namespace Cads.Cds.BuildingBlocks.Testing.Support.TestFixtures.Containers;

using IContainer = DotNet.Testcontainers.Containers.IContainer;

// ReSharper disable once ClassNeverInstantiated.Global
public class ApiContainerFixture : IAsyncLifetime
{
    public IContainer ApiContainer { get; private set; } = null!;
    private HttpClient HttpClient { get; set; } = null!;
    public PostgresFixture PostgresFixture { get; } = new();
    public LocalStackFixture LocalStackFixture { get; } = new();
    public OidcMockFixture OidcMockFixture { get; } = new();
    public TestAzureAdConfiguration? AzureAdConfig { get; set; }

    public async ValueTask InitializeAsync()
    {
        await PostgresFixture.InitializeAsync();
        await LocalStackFixture.InitializeAsync();
        await OidcMockFixture.InitializeAsync();

        AzureAdConfig = new TestAzureAdConfiguration(OidcMockFixture);

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
          .WithEnvironment("Modules__StorageBridge__Storage__CadsExternal__BucketName", LocalStackFixture.CadsExternalBucketName)
          .WithEnvironment("Modules__StorageBridge__Storage__CadsExternal__AccessKeySecretName", "IMB_S3_ACCESS_KEY")
          .WithEnvironment("Modules__StorageBridge__Storage__CadsExternal__SecretKeySecretName", "IMB_S3_SECRET_KEY")
          .WithEnvironment("IMB_S3_ACCESS_KEY", "test")
          .WithEnvironment("IMB_S3_SECRET_KEY", "test")
          .WithEnvironment("Modules__Ingester__Storage__CadsIngester__BucketName", LocalStackFixture.CadsInternalBucketName)
          .WithEnvironment("Modules__Ingester__Queues__CadsCds__QueueUrl", LocalStackFixture.CadsQueueUrl)
          .WithEnvironment("Modules__Ingester__Queues__CadsCds__DlqQueueUrl", LocalStackFixture.CadsDeadLetterQueueUrl)
          .WithEnvironment("LOCALSTACK_ENDPOINT", LocalStackFixture.NetworkServiceUrl)
          .WithEnvironment("Postgres__DefaultConnection", PostgresFixture.ConnectionString)
          .WithEnvironment("Postgres__ReadOnlyConnection", PostgresFixture.ReadConnectionString)
          .WithEnvironment("AuthenticationConfiguration__ApiKey__Enabled", "true")
          .WithEnvironment("AuthenticationConfiguration__Cognito__Enabled", "false")
          .WithEnvironment("AuthenticationConfiguration__Cognito__Authority", "")
          .WithEnvironment("AuthenticationConfiguration__AzureAD__Enabled", "true")
          .WithEnvironment("AuthenticationConfiguration__AzureAD__Authority", AzureAdConfig.ContainerAuthority)
          .WithEnvironment("AuthenticationConfiguration__AzureAD__Audience", AzureAdConfig.Audience)
          .WithEnvironment("AuthenticationConfiguration__AzureAD__MetadataAddress", AzureAdConfig.ContainerMetadataAddress)
          .WithEnvironment("AuthenticationConfiguration__AzureAD__RequireHttpsMetadata", AzureAdConfig.RequireHttpsMetadata.ToString())
          .WithEnvironment("AuthenticationConfiguration__AzureAD__ValidateIssuer", "false")
          .WithEnvironment("AuthenticationConfiguration__AzureAD__RoleClaimType", "scope")
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

        var handler = new HttpClientHandler
        {
            ServerCertificateCustomValidationCallback = (sender, cert, chain, errors) => true
        };
    }

    public async Task<HttpClient> CreateAzureAdClientAsync(TestTokenRequest? request = null)
    {
        request ??= new TestTokenRequest(); // default = client_credentials

        var token = await OidcMockFixture.CreateTokenAsync(request);

        var client = new HttpClient
        {
            BaseAddress = HttpClient.BaseAddress
        };

        client.DefaultRequestHeaders.Authorization =
            new AuthenticationHeaderValue("Bearer", token);

        return client;
    }

    public HttpClient CreateBasicClient()
    {
        var client = new HttpClient { BaseAddress = HttpClient.BaseAddress };
        client.AddBasicApiKey(TestAuthConstants.BasicApiKey, TestAuthConstants.BasicSecret);
        return client;
    }

    public HttpClient CreateClient()
    {
        return HttpClient;
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
        await Safe(() => OidcMockFixture.DisposeAsync());

        try { HttpClient?.Dispose(); }
        catch (Exception ex) { error ??= ex; }

        await Safe(() => ApiContainer.DisposeAsync());

        GC.SuppressFinalize(this);

        if (error is not null)
            throw error;
    }
}