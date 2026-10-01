using Cads.Cds.BuildingBlocks.Testing.Support.Constants;
using Cads.Cds.BuildingBlocks.Testing.Support.Fakes.Authentication;

namespace Cads.Cds.MiBff.Testing.Support.Factories.Authorization;

public static class TestReportsReadTokenFactory
{
    private const string ReportsNoneScope = "reports.none";

    private static readonly string[] s_baseScopes = ["openid", "profile", "email"];

    public static TestTokenRequest ValidUserToken() =>
        UserToken(
            TestAuthConstants.AzureAdCadsMisUsername,
            TestAuthConstants.AzureAdPassword,
            TestAuthConstants.AzureAdReportsReadScope);

    public static TestTokenRequest MissingScopeToken() =>
        UserToken(
            TestAuthConstants.AzureAdCadsMisUsername,
            TestAuthConstants.AzureAdPassword,
            TestAuthConstants.AzureAdDbAdminExecuteScope); // valid audience, but no reports.read

    public static TestTokenRequest InvalidScopeToken() =>
        UserToken(
            TestAuthConstants.AzureAdCadsMisUsername,
            TestAuthConstants.AzureAdPassword,
            ReportsNoneScope);

    public static TestTokenRequest ForUser(string username) =>
        UserToken(
            username,
            TestAuthConstants.AzureAdPassword,
            TestAuthConstants.AzureAdReportsReadScope);

    private static TestTokenRequest UserToken(
        string username,
        string password,
        params string[] scopes) =>
        new()
        {
            ClientId = TestAuthConstants.AzureAdTestUserClientId,
            ClientSecret = TestAuthConstants.AzureAdTestUserClientSecret,
            Username = username,
            Password = password,
            Scopes = [.. s_baseScopes, .. scopes]
        };
}
