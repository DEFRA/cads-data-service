using Cads.Cds.BuildingBlocks.Testing.Support.Utilities.Http;
using Cads.Cds.MiBff.Controllers.Requests.Reports;
using Cads.Cds.MiBff.Tests.Component.TestFixtures;
using FluentAssertions;

namespace Cads.Cds.MiBff.Tests.Component.Reports;

public class GetScrapieFlockSchemeAuditReportTests(MiBffTestFixture testFixture) : IClassFixture<MiBffTestFixture>
{
    private readonly MiBffTestFixture _testFixture = testFixture;

    [Fact]
    public async Task GivenValidRequest_GetScrapieFlockSchemeAuditReport_ShouldSucceed()
    {
        var endpoint = "/api/v1/bff/mi/reports/scrapie_flock_scheme_audit";

        var request = new GetPlaceholderReportRequest { ReportKey = "scrapie_flock_scheme_audit" };
        var payload = HttpContentUtility.CreateApplicationJsonAsStringContent(request);

        var response = await _testFixture.HttpClient.PostAsync(endpoint, payload, TestContext.Current.CancellationToken);
        response.IsSuccessStatusCode.Should().BeTrue();
    }
}