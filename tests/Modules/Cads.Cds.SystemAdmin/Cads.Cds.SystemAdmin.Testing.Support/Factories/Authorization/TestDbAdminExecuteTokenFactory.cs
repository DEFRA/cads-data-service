using Cads.Cds.BuildingBlocks.Testing.Support.Constants;
using Cads.Cds.BuildingBlocks.Testing.Support.Fakes.Authentication;
using Cads.Cds.BuildingBlocks.Testing.Support.Utilities.Authorization;

namespace Cads.Cds.SystemAdmin.Testing.Support.Factories.Authorization;

public static class TestDbAdminExecuteTokenFactory
{
    public static TestTokenRequest ValidUserToken() =>
        TestTokenFactory.UserToken(
            TestAuthConstants.AzureAdCadsAdminUsername,
            TestAuthConstants.AzureAdPassword,
            TestAuthConstants.AzureAdDbAdminExecuteScope);

    public static TestTokenRequest MissingRoleToken() =>
        TestTokenFactory.UserToken(
            TestAuthConstants.AzureAdCadsMisUsername,
            TestAuthConstants.AzureAdPassword,
            TestAuthConstants.AzureAdDbAdminExecuteScope); // valid user, but no cads-admin-superuser role

    public static TestTokenRequest MissingScopeToken() =>
        TestTokenFactory.UserToken(
            TestAuthConstants.AzureAdCadsAdminUsername,
            TestAuthConstants.AzureAdPassword,
            TestAuthConstants.AzureAdReportsReadScope);    // valid audience, but no admin.db.execute
}