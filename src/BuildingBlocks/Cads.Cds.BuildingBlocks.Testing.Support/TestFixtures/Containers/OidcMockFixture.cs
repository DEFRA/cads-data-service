namespace Cads.Cds.BuildingBlocks.Testing.Support.TestFixtures.Containers;

using Cads.Cds.BuildingBlocks.Testing.Support.Constants;
using Cads.Cds.BuildingBlocks.Testing.Support.Fakes.Authentication;
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
                "ClientId": "{{TestAuthConstants.AzureAdCadsMisClientId}}",
                "ClientSecrets": ["{{TestAuthConstants.AzureAdCadsMisClientSecret}}"],
                "AllowedGrantTypes": ["authorization_code"],
                "AllowedScopes": ["openid", "profile", "email", "offline_access", "{{TestAuthConstants.AzureAdCadsCdsScope}}"],
                "RequirePkce": false,
                "RedirectUris": ["http://localhost:3000"],
                "AllowOfflineAccess": true,
                "AccessTokenType": "Jwt",
                "AlwaysSendClientClaims": true,
                "AlwaysIncludeUserClaimsInIdToken": true
              },
              {
                "ClientId": "{{TestAuthConstants.AzureAdCadsApiClientId}}",
                "ClientSecrets": ["{{TestAuthConstants.AzureAdCadsApiClientSecret}}"],
                "AllowedGrantTypes": ["client_credentials"],
                "AllowedScopes": ["{{TestAuthConstants.AzureAdCadsCdsScope}}"],
                "AccessTokenType": "Jwt",
                "AlwaysSendClientClaims": true
              },
              {
                "ClientId": "{{TestAuthConstants.AzureAdTestUserClientId}}",
                "ClientSecrets": ["{{TestAuthConstants.AzureAdTestUserClientSecret}}"],
                "AllowedGrantTypes": ["password"],
                "AllowedScopes": ["openid", "profile", "email", "reports.none", "{{TestAuthConstants.AzureAdCadsCdsScope}}"],
                "AccessTokenType": "Jwt",
                "AlwaysSendClientClaims": true,
                "AlwaysIncludeUserClaimsInIdToken": true
              }
            ]
            """)
            .WithEnvironment("SCOPES_CONFIGURATION_INLINE", $$"""
            [
              {
                "Name": "{{TestAuthConstants.AzureAdCadsCdsScope}}",
                "DisplayName": "Read reports"
              },
              {
                "Name": "reports.none",
                "DisplayName": "No report access"
              }
            ]
            """)
            .WithEnvironment("API_SCOPES_INLINE", $$"""
            [
              {
                "Name": "{{TestAuthConstants.AzureAdCadsCdsScope}}",
                "DisplayName": "Read reports"
              },
              {
                "Name": "reports.none",
                "DisplayName": "No report access"
              }
            ]
            """)
            .WithEnvironment("API_RESOURCES_INLINE", $$"""
            [
              {
                "Name": "{{TestAuthConstants.AzureAdCadsCdsAudience}}",
                "Scopes": ["{{TestAuthConstants.AzureAdCadsCdsScope}}", "reports.none"],
                "UserClaims": [
                  "email",
                  "preferred_username",
                  "name",
                  "role"
                ]
              }
            ]
            """)
            .WithEnvironment("USERS_CONFIGURATION_INLINE", $$"""
            [
                {
                "SubjectId": "787553ae-4c55-4b42-aafc-633274691cc1",
                "Username": "{{TestAuthConstants.AzureAdUsername}}",
                "Password": "{{TestAuthConstants.AzureAdPassword}}",
                "Claims": [
                    { "Type": "name", "Value": "Test MIP Viewer" },
                    { "Type": "email", "Value": "mip-viewer-user@internal.test" },
                    { "Type": "preferred_username", "Value": "mip-viewer-user" },
                    { "Type": "role", "Value": "mip-viewer" }
                ]
                },
                {
                "SubjectId": "c6c53dda-b6d9-4599-a90d-dbb3121cf737",
                "Username": "unknown-user",
                "Password": "{{TestAuthConstants.AzureAdPassword}}",
                "Claims": [
                    { "Type": "name", "Value": "Unknown User" },
                    { "Type": "email", "Value": "unknown-user@internal.test" },
                    { "Type": "preferred_username", "Value": "unknown-user" },
                    { "Type": "role", "Value": "mip-viewer" }
                ]
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

    public async Task<string> CreateTokenAsync(TestTokenRequest request)
    {
        using var http = new HttpClient();

        var form = new Dictionary<string, string>
        {
            ["client_id"] = request.ClientId,
            ["client_secret"] = request.ClientSecret,
            ["scope"] = string.Join(" ", request.Scopes)
        };

        if (request.Username is null)
        {
            // client_credentials
            form["grant_type"] = "client_credentials";
        }
        else
        {
            // password (user token)
            form["grant_type"] = "password";
            form["username"] = request.Username;
            form["password"] = request.Password!;
        }

        var response = await http.PostAsync(TokenEndpoint, new FormUrlEncodedContent(form));
        response.EnsureSuccessStatusCode();

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