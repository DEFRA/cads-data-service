using Cads.Cds.BuildingBlocks.Testing.Support.Constants;
using Cads.Cds.BuildingBlocks.Testing.Support.Fakes.Authentication;

namespace Cads.Cds.BuildingBlocks.Testing.Support.Utilities.Authorization;

public static class TestTokenFactory
{
    public static TestTokenRequest ValidUserToken() =>
        new()
        {
            ClientId = TestAuthConstants.AzureAdTestUserClientId,
            ClientSecret = TestAuthConstants.AzureAdTestUserClientSecret,
            Username = TestAuthConstants.AzureAdUsername,
            Password = TestAuthConstants.AzureAdPassword,
            Scopes =
            [
                "openid",
                "profile",
                "email",
                TestAuthConstants.AzureAdCadsCdsScope
            ]
        };

    public static TestTokenRequest MissingScopeToken() =>
        new()
        {
            ClientId = TestAuthConstants.AzureAdTestUserClientId,
            ClientSecret = TestAuthConstants.AzureAdTestUserClientSecret,
            Username = TestAuthConstants.AzureAdUsername,
            Password = TestAuthConstants.AzureAdPassword,
            Scopes = ["openid", "profile", "email"]
        };

    public static TestTokenRequest InvalidScopeToken() =>
        new()
        {
            ClientId = TestAuthConstants.AzureAdTestUserClientId,
            ClientSecret = TestAuthConstants.AzureAdTestUserClientSecret,
            Username = TestAuthConstants.AzureAdUsername,
            Password = TestAuthConstants.AzureAdPassword,
            Scopes = ["openid", "profile", "email", "reports.none"]
        };

    public static TestTokenRequest ForUser(string username) =>
        new()
        {
            ClientId = TestAuthConstants.AzureAdTestUserClientId,
            ClientSecret = TestAuthConstants.AzureAdTestUserClientSecret,
            Username = username,
            Password = TestAuthConstants.AzureAdPassword,
            Scopes =
            [
                "openid",
                "profile",
                "email",
                TestAuthConstants.AzureAdCadsCdsScope
            ]
        };

    public static TestTokenRequest DbAdminExecuteToken() =>
        new()
        {
            ClientId = TestAuthConstants.AzureAdTestUserClientId,
            ClientSecret = TestAuthConstants.AzureAdTestUserClientSecret,
            Username = TestAuthConstants.AzureAdDbAdminUsername,
            Password = TestAuthConstants.AzureAdPassword,
            Scopes =
            [
                "openid",
                "profile",
                "email",
                TestAuthConstants.AzureAdCadsCdsDbAdminScope
            ]
        };

    // Valid role (cads-admin-superuser), but token requests reports.read instead of db.admin.execute.
    // "reports.read" is still tied to the api://local-cads-cds resource, so audience validation still passes,
    // isolating this purely to a missing scope claim.
    public static TestTokenRequest DbAdminMissingScopeToken() =>
        new()
        {
            ClientId = TestAuthConstants.AzureAdTestUserClientId,
            ClientSecret = TestAuthConstants.AzureAdTestUserClientSecret,
            Username = TestAuthConstants.AzureAdDbAdminUsername,
            Password = TestAuthConstants.AzureAdPassword,
            Scopes =
            [
                "openid",
                "profile",
                "email",
                TestAuthConstants.AzureAdCadsCdsScope
            ]
        };

    // Valid scope, but authenticated as a user without the cads-admin-superuser role claim.
    public static TestTokenRequest DbAdminMissingRoleToken() =>
        new()
        {
            ClientId = TestAuthConstants.AzureAdTestUserClientId,
            ClientSecret = TestAuthConstants.AzureAdTestUserClientSecret,
            Username = TestAuthConstants.AzureAdUsername,
            Password = TestAuthConstants.AzureAdPassword,
            Scopes =
            [
                "openid",
                "profile",
                "email",
                TestAuthConstants.AzureAdCadsCdsDbAdminScope
            ]
        };
}