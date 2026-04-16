using Cads.Cds.BuildingBlocks.Testing.Support.Utilities.Http;
using Cads.Cds.MiBff.Controllers.Requests.Reports;
using Cads.Cds.MiBff.Core.DTOs.Reports;
using Cads.Cds.MiBff.Tests.Component.TestFixtures;
using FluentAssertions;
using System.Net.Http.Json;

namespace Cads.Cds.MiBff.Tests.Component.Reports;

public class ReportsControllerTests(MiBffTestFixture testFixture) : IClassFixture<MiBffTestFixture>
{
    private readonly MiBffTestFixture _testFixture = testFixture;

    [Fact]
    public async Task GivenValidRequest_GetUserReports_ShouldSucceed()
    {
        var endpoint = "/api/v1/bff/mi/reports";

        var response = await _testFixture.HttpClient.GetAsync(endpoint, TestContext.Current.CancellationToken);
        response.IsSuccessStatusCode.Should().BeTrue();

        var data = await response.Content.ReadFromJsonAsync<List<ReportDto>>(TestContext.Current.CancellationToken);
        data.Should().NotBeNull().And.HaveCount(2);
    }

    [Fact]
    public async Task GivenValidRequest_GetHoldingSummaryReport_ShouldSucceed()
    {
        var endpoint = "/api/v1/bff/mi/reports/holding_summary";

        var request = new GetHoldingSummaryReportRequest();
        var payload = HttpContentUtility.CreateResponseContent(request);

        var response = await _testFixture.HttpClient.PostAsync(endpoint, payload, TestContext.Current.CancellationToken);
        response.IsSuccessStatusCode.Should().BeTrue();
    }
}