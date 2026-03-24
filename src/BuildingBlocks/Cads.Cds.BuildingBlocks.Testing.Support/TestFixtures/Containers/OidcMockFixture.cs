namespace Cads.Cds.BuildingBlocks.Testing.Support.TestFixtures.Containers;

using Cads.Cds.BuildingBlocks.Testing.Support.Constants;
using DotNet.Testcontainers.Builders;
using System.Text.Json;
using Xunit;

using IContainer = DotNet.Testcontainers.Containers.IContainer;

// ReSharper disable once ClassNeverInstantiated.Global
public class OidcMockFixture : IAsyncLifetime
{
    public IContainer OidcContainer { get; private set; } = null!;
    public int OidcPort => OidcContainer.GetMappedPublicPort(80);

    public string Issuer => $"http://localhost:{OidcPort}";
    public string WellKnown => $"{Issuer}/.well-known/openid-configuration";
    public string JwksUri => $"{Issuer}/jwks";
    public string TokenEndpoint => $"{Issuer}/connect/token";

    public async ValueTask InitializeAsync()
    {
        DockerNetworkHelper.EnsureNetworkExists(TestContainerConstants.NetworkName);

        OidcContainer = new ContainerBuilder("ghcr.io/soluto/oidc-server-mock:0.6.0")
            .WithName($"cads-oidc-mock-{Guid.NewGuid()}")
            .WithPortBinding(80, true)
            .WithEnvironment("ASPNETCORE_ENVIRONMENT", "Development")
            .WithEnvironment("CLIENTS_CONFIGURATION_INLINE", $$"""
            [
              {
                "ClientId": "{{TestAuthConstants.AzureAdClientId}}",
                "ClientSecrets": ["{{TestAuthConstants.AzureAdClientSecret}}"],
                "AllowedScopes": ["{{TestAuthConstants.AzureAdScope}}"],
                "AllowedGrantTypes": ["client_credentials"]
              }
            ]
            """)
            .WithEnvironment("SCOPES_CONFIGURATION_INLINE", $$"""
            [
              {
                "Name": "{{TestAuthConstants.AzureAdScope}}",
                "DisplayName": "Read reports"
              }
            ]
            """)
            .WithEnvironment("API_SCOPES_INLINE", $$"""
            [
              {
                "Name": "{{TestAuthConstants.AzureAdScope}}",
                "DisplayName": "Read reports"
              }
            ]
            """)
            .WithEnvironment("API_RESOURCES_INLINE", $$"""
            [
              {
                "Name": "{{TestAuthConstants.AzureAdAudience}}",
                "Scopes": ["{{TestAuthConstants.AzureAdScope}}"]
              }
            ]
            """)
            .WithEnvironment("ISSUER", "http://cads-oidc-mock")
            .WithNetwork(TestContainerConstants.NetworkName)
            .WithNetworkAliases("cads-oidc-mock")
            .WithWaitStrategy(Wait.ForUnixContainer()
                .UntilHttpRequestIsSucceeded(req => req.ForPort(80).ForPath("/.well-known/openid-configuration"), o => o.WithTimeout(TimeSpan.FromSeconds(25)))
            )
            .Build();

        await OidcContainer.StartAsync();
    }

    public async Task<string> CreateClientCredentialsTokenAsync()
    {
        using var http = new HttpClient();

        var response = await http.PostAsync(TokenEndpoint,
            new FormUrlEncodedContent(new Dictionary<string, string>
            {
                ["grant_type"] = "client_credentials",
                ["client_id"] = TestAuthConstants.AzureAdClientId,
                ["client_secret"] = TestAuthConstants.AzureAdClientSecret,
                ["scope"] = TestAuthConstants.AzureAdScope
            }));

        var json = await response.Content.ReadAsStringAsync();
        return JsonDocument.Parse(json).RootElement.GetProperty("access_token").GetString()!;
    }

    public async ValueTask DisposeAsync()
    {
        Exception? error = null;

        async ValueTask Safe(Func<ValueTask> f)
        {
            try { await f(); }
            catch (Exception ex) { error ??= ex; }
        }

        await Safe(() => OidcContainer.DisposeAsync());

        GC.SuppressFinalize(this);

        if (error is not null)
            throw error;
    }
}