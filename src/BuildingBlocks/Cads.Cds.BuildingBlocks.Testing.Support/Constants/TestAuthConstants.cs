namespace Cads.Cds.BuildingBlocks.Testing.Support.Constants;

public static class TestAuthConstants
{
    // Basic API key
    public const string BasicApiKey = "ApiKey";
    public const string BasicSecret = "integration-test-secret";

    // Azure AD / OIDC mock
    public const string AzureAdClientId = "local-cads-mis";
    public const string AzureAdClientSecret = "local-mock-secret";
    public const string AzureAdAudience = "api://local-cads-mis";
    public const string AzureAdScope = "reports.read";

    // Fakes: Cognito
    public const string FakeCongnitoAuthority = "https://cognito-test";

    // Fakes: Azure AD
    public const string AzureAdFakeAuthority = "https://fake-issuer";
}