using Cads.Cds.BuildingBlocks.Testing.Support.Utilities.Http;
using Cads.Cds.MiBff.Controllers.Requests.Reports;
using Cads.Cds.MiBff.Tests.Component.TestFixtures;
using FluentAssertions;

namespace Cads.Cds.MiBff.Tests.Component.Reports;

public class GetMovementsAllHoldingsReportTests(MiBffTestFixture testFixture) : IClassFixture<MiBffTestFixture>
{
    private readonly MiBffTestFixture _testFixture = testFixture;

    [Fact]
    public async Task GivenValidRequest_GetMovementsAllHoldingsReport_ShouldSucceed()
    {
        var endpoint = "/api/v1/bff/mi/reports/movements_all_holdings";

        var request = new GetPlaceholderReportRequest { ReportKey = "movements_all_holdings" };
        var payload = HttpContentUtility.CreateApplicationJsonAsStringContent(request);

        var response = await _testFixture.HttpClient.PostAsync(endpoint, payload, TestContext.Current.CancellationToken);
        response.IsSuccessStatusCode.Should().BeTrue();
    }
}
