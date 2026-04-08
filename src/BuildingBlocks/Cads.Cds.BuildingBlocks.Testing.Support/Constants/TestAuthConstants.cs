namespace Cads.Cds.BuildingBlocks.Testing.Support.Constants;

public static class TestAuthConstants
{
    // Basic API key
    public const string BasicApiKey = "ApiKey";
    public const string BasicSecret = "integration-test-secret";

    // Azure AD / OIDC mock
    public const string AzureAdCadsMisClientId = "local-cads-mis";
    public const string AzureAdCadsMisClientSecret = "local-mock-secret";
    public const string AzureAdCadsCdsAudience = "api://local-cads-cds";
    public const string AzureAdCadsCdsScope = "reports.read";
    public const string AzureAdCadsMisUsername = "mip-viewer-user";
    public const string AzureAdCadsMisPassword = "password";

    // Fakes: Cognito
    public const string FakeCongnitoAuthority = "https://cognito-test";

    // Fakes: Azure AD
    public const string AzureAdFakeAuthority = "https://fake-issuer";
}