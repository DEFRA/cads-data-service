using Cads.Cds.BuildingBlocks.Testing.Support.Constants;
using Cads.Cds.BuildingBlocks.Testing.Support.TestFixtures.Containers;
using Cads.Cds.BuildingBlocks.Testing.Support.Utilities.Authorization;

namespace Cads.Cds.MiBff.Tests.Integration.Reports;

[Collection("MiBffIntegration"), Trait("Dependence", "testcontainers")]
public class GetUserReportPermissionsTests(ApiContainerFixture apiContainerFixture)
{
    [Fact]
    public async Task GivenValidUser_WhenGetHoldingSummaryReportRequested_ShouldSucceed()
    {
        var endpoint = String.Format(TestEndpointConstants.BffMiUserReportPermissionsEndpoint, "holding_summary");
        var client = await apiContainerFixture.CreateAzureAdClientAsync(TestTokenFactory.ValidUserToken());

        var response = await client.GetAsync(endpoint, TestContext.Current.CancellationToken);
        response.EnsureSuccessStatusCode();
    }
}