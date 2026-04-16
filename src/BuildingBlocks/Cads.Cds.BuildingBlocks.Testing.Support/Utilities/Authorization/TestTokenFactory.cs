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
}
