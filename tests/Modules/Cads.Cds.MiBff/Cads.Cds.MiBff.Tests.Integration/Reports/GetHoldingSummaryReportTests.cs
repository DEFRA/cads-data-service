using Cads.Cds.BuildingBlocks.Testing.Support.Constants;
using Cads.Cds.BuildingBlocks.Testing.Support.TestFixtures.Containers;
using Cads.Cds.BuildingBlocks.Testing.Support.Utilities.Authorization;
using Cads.Cds.BuildingBlocks.Testing.Support.Utilities.Http;

namespace Cads.Cds.MiBff.Tests.Integration.Reports;

[Collection("MiBffIntegration"), Trait("Dependence", "testcontainers")]
public class GetHoldingSummaryReportTests(ApiContainerFixture apiContainerFixture)
{
    [Fact]
    public async Task GivenValidUser_WhenGetHoldingSummaryReportRequested_ShouldSucceed()
    {
        var endpoint = TestEndpointConstants.BffMiReportsHoldingSummaryRoot;
        var client = await apiContainerFixture.CreateAzureAdClientAsync(TestTokenFactory.ValidUserToken());

        var payload = HttpContentUtility.CreateApplicationJsonAsStringContent("{\"reportKey\":\"holding_summary\"}");

        var response = await client.PostAsync(endpoint, payload, TestContext.Current.CancellationToken);
        response.EnsureSuccessStatusCode();
    }
}
