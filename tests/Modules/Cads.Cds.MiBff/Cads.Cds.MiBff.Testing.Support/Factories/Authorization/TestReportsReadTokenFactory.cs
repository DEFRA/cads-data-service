using Cads.Cds.BuildingBlocks.Testing.Support.Constants;
using Cads.Cds.BuildingBlocks.Testing.Support.Fakes.Authentication;
using Cads.Cds.BuildingBlocks.Testing.Support.Utilities.Authorization;

namespace Cads.Cds.MiBff.Testing.Support.Factories.Authorization;

public static class TestReportsReadTokenFactory
{
    private const string ReportsNoneScope = "reports.none";

    public static TestTokenRequest ValidUserToken() =>
        TestTokenFactory.UserToken(
            TestAuthConstants.AzureAdCadsMisUsername,
            TestAuthConstants.AzureAdPassword,
            TestAuthConstants.AzureAdReportsReadScope);

    public static TestTokenRequest MissingScopeToken() =>
        TestTokenFactory.UserToken(
            TestAuthConstants.AzureAdCadsMisUsername,
            TestAuthConstants.AzureAdPassword,
            TestAuthConstants.AzureAdDbAdminExecuteScope); // valid audience, but no reports.read

    public static TestTokenRequest InvalidScopeToken() =>
        TestTokenFactory.UserToken(
            TestAuthConstants.AzureAdCadsMisUsername,
            TestAuthConstants.AzureAdPassword,
            ReportsNoneScope);

    public static TestTokenRequest ForUser(string username) =>
        TestTokenFactory.UserToken(
            username,
            TestAuthConstants.AzureAdPassword,
            TestAuthConstants.AzureAdReportsReadScope);
}
