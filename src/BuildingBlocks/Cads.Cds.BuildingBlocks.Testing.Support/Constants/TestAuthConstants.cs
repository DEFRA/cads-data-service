namespace Cads.Cds.BuildingBlocks.Testing.Support.Constants;

public static class TestAuthConstants
{
    // # Basic ApiKey
    public const string BasicApiKey = "ApiKey";
    public const string BasicSecret = "integration-test-secret";

    // # Audience
    public const string AzureAdCadsCdsAudience = "api://local-cads-cds";

    // # Scopes
    public const string AzureAdReportsReadScope = "reports.read";
    public const string AzureAdDbAdminExecuteScope = "db.admin.execute";
    public const string AzureAdAdminS3ManagerScope = "admin.s3.manager";
    public const string AzureAdAdminQueueManagerScope = "admin.queue.manager";

    // # Azure AD / OIDC mock

    // ## Service clients

    // ### TestService (GrantType: client_credentials)
    public const string AzureAdTestServiceClientId = "local-test-service-client";
    public const string AzureAdTestServiceClientSecret = "local-mock-secret";

    // ### TestUser (GrantType: password)
    public const string AzureAdTestUserClientId = "local-cads-test-user-client";
    public const string AzureAdTestUserClientSecret = "local-mock-secret";

    // # Users
    public const string AzureAdPassword = "password";

    // ## cads-mis test user
    public const string AzureAdCadsMisEmail = "mip-viewer-user@internal.test";
    public const string AzureAdCadsMisUsername = "mip-viewer-user";

    // ## cads-admin-frontend test user
    public const string AzureAdCadsAdminEmail = "cads-admin-user@internal.test";
    public const string AzureAdCadsAdminUsername = "cads-admin-user";

    // # Fakes

    // ## Fakes: bearer token control values (consumed by FakeJwtHandler)
    public const string FakeJwtDefault = "fake-jwt-token";
    public const string FakeJwtMissingDbAdminRole = "fake-jwt-token-missing-role";
    public const string FakeJwtMissingDbAdminScope = "fake-jwt-token-missing-scope";
    public const string FakeJwtMissingS3AdminScope = "fake-jwt-token-missing-s3-admin-scope";

    // ## Fakes: Cognito
    public const string FakeCongnitoAuthority = "https://cognito-test";

    // ## Fakes: Azure AD
    public const string AzureAdFakeAuthority = "https://fake-issuer";
}