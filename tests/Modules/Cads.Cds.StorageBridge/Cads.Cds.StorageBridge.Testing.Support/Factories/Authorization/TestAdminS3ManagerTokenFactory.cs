using Cads.Cds.BuildingBlocks.Testing.Support.Constants;
using Cads.Cds.BuildingBlocks.Testing.Support.Fakes.Authentication;
using Cads.Cds.BuildingBlocks.Testing.Support.Utilities.Authorization;

namespace Cads.Cds.StorageBridge.Testing.Support.Factories.Authorization;

public static class TestAdminS3ManagerTokenFactory
{
    public static TestTokenRequest ValidUserToken() =>
        TestTokenFactory.UserToken(
            TestAuthConstants.AzureAdCadsAdminUsername,
            TestAuthConstants.AzureAdPassword,
            TestAuthConstants.AzureAdAdminS3ManagerScope);

    public static TestTokenRequest MissingScopeToken() =>
        TestTokenFactory.UserToken(
            TestAuthConstants.AzureAdCadsAdminUsername,
            TestAuthConstants.AzureAdPassword,
            TestAuthConstants.AzureAdDbAdminExecuteScope); // valid audience, but no admin.s3.manager
}