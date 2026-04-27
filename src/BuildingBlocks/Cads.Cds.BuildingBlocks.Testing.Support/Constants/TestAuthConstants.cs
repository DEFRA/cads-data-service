namespace Cads.Cds.BuildingBlocks.Testing.Support.Constants;

public static class TestAuthConstants
{
    // Basic API key
    public const string BasicApiKey = "ApiKey";
    public const string BasicSecret = "integration-test-secret";

    // Azure AD / OIDC mock

    // UI client
    public const string AzureAdCadsMisClientId = "local-cads-mis";
    public const string AzureAdCadsMisClientSecret = "local-mock-secret";

    // Service-to-service client
    public const string AzureAdCadsApiClientId = "local-cads-api-client";
    public const string AzureAdCadsApiClientSecret = "local-mock-secret";

    // Test user client
    public const string AzureAdTestUserClientId = "local-cads-test-user-client";
    public const string AzureAdTestUserClientSecret = "local-mock-secret";

    // Test user
    public const string AzureAdEmail = "mip-viewer-user@internal.test";
    public const string AzureAdUsername = "mip-viewer-user";
    public const string AzureAdPassword = "password";

    // Audience & Scopes
    public const string AzureAdCadsCdsAudience = "api://local-cads-cds";
    public const string AzureAdCadsCdsScope = "reports.read";

    // Fakes

    // Fakes: Cognito
    public const string FakeCongnitoAuthority = "https://cognito-test";

    // Fakes: Azure AD
    public const string AzureAdFakeAuthority = "https://fake-issuer";
}