using Cads.Cds.BuildingBlocks.Testing.Support.Constants;
using Cads.Cds.BuildingBlocks.Testing.Support.Fakes.Authentication;
using Cads.Cds.BuildingBlocks.Testing.Support.Utilities.Authorization;

namespace Cads.Cds.SystemAdmin.Testing.Support.Factories.Authorization;

public static class TestSqsAdminExecuteTokenFactory
{
    public static TestTokenRequest ValidUserToken() =>
        TestTokenFactory.UserToken(
            TestAuthConstants.AzureAdCadsAdminUsername,
            TestAuthConstants.AzureAdPassword,
            TestAuthConstants.AzureAdAdminQueueManagerScope);
    
    public static TestTokenRequest MissingScopeToken() =>
        TestTokenFactory.UserToken(
            TestAuthConstants.AzureAdCadsAdminUsername,
            TestAuthConstants.AzureAdPassword,
            TestAuthConstants.AzureAdReportsReadScope);    // valid audience, but no admin.queue.manager
}
