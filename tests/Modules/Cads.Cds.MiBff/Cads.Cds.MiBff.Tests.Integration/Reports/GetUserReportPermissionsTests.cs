using Cads.Cds.BuildingBlocks.Testing.Support.TestFixtures.Containers;
using Cads.Cds.MiBff.Testing.Support.Constants;
using Cads.Cds.MiBff.Testing.Support.Factories.Authorization;
using FluentAssertions;

namespace Cads.Cds.MiBff.Tests.Integration.Reports;

[Collection("MiBffIntegration"), Trait("Dependence", "testcontainers")]
public class GetUserReportPermissionsTests(ApiContainerFixture apiContainerFixture)
{
    [Fact]
    public async Task GivenValidUser_WhenGetHoldingSummaryReportRequested_ShouldSucceed()
    {
        var endpoint = string.Format(TestEndpointConstants.BffMiReportsUserReportPermissionsEndpoint, TestReportKeyConstants.HoldingSummaryReportKey);
        var client = await apiContainerFixture.CreateAzureAdClientAsync(TestReportsReadTokenFactory.ValidUserToken());

        var response = await client.GetAsync(endpoint, TestContext.Current.CancellationToken);
        response.IsSuccessStatusCode.Should().BeTrue();
    }
}