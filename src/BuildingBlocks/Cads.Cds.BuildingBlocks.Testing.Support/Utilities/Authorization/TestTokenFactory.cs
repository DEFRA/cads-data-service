using Cads.Cds.BuildingBlocks.Testing.Support.Constants;
using Cads.Cds.BuildingBlocks.Testing.Support.Fakes.Authentication;

namespace Cads.Cds.BuildingBlocks.Testing.Support.Utilities.Authorization;

public static class TestTokenFactory
{
    private static readonly string[] s_baseScopes = ["openid", "profile", "email"];

    public static TestTokenRequest UserToken(
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