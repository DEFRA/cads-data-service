using Cads.Cds.BuildingBlocks.Testing.Support.Constants;
using Cads.Cds.BuildingBlocks.Testing.Support.Fakes.Authentication;

namespace Cads.Cds.SystemAdmin.Testing.Support.Factories.Authorization;

public static class TestDbAdminExecuteTokenFactory
{
    private static readonly string[] s_baseScopes = ["openid", "profile", "email"];

    public static TestTokenRequest ValidUserToken() =>
        UserToken(
            TestAuthConstants.AzureAdCadsAdminUsername,
            TestAuthConstants.AzureAdPassword,
            TestAuthConstants.AzureAdDbAdminExecuteScope);

    public static TestTokenRequest MissingScopeToken() =>
        UserToken(
            TestAuthConstants.AzureAdCadsAdminUsername,
            TestAuthConstants.AzureAdPassword,
            TestAuthConstants.AzureAdReportsReadScope); // valid audience, but no db.admin.execute

    public static TestTokenRequest MissingRoleToken() =>
        UserToken(
            TestAuthConstants.AzureAdCadsMisUsername,
            TestAuthConstants.AzureAdPassword,
            TestAuthConstants.AzureAdDbAdminExecuteScope);

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
