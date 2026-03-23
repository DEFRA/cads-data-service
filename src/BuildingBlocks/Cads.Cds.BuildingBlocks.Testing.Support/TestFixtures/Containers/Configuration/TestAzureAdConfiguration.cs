using Cads.Cds.BuildingBlocks.Infrastructure.Authentication.Configuration;
using Cads.Cds.BuildingBlocks.Testing.Support.Constants;

namespace Cads.Cds.BuildingBlocks.Testing.Support.TestFixtures.Containers.Configuration;

public class TestAzureAdConfiguration : AuthenticationProviderConfiguration
{
    public string ContainerAuthority { get; }
    public string ContainerMetadataAddress { get; }

    public TestAzureAdConfiguration(OidcMockFixture oidc)
    {
        Authority = oidc.Issuer;
        Audience = TestAuthConstants.AzureAdAudience;
        MetadataAddress = oidc.WellKnown;
        RequireHttpsMetadata = false;

        // For the API container (inside Docker)
        ContainerAuthority = "http://cads-oidc-mock";
        ContainerMetadataAddress = "http://cads-oidc-mock/.well-known/openid-configuration";
    }
}