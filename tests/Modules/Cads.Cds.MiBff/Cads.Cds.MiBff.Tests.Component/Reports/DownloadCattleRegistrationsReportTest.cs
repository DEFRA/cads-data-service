using Cads.Cds.BuildingBlocks.Testing.Support.Utilities.Http;
using Cads.Cds.MiBff.Controllers.Requests.Reports;
using Cads.Cds.MiBff.Tests.Component.TestFixtures;
using FluentAssertions;

namespace Cads.Cds.MiBff.Tests.Component.Reports;

public class DownloadCattleRegistrationsReportTest(MiBffTestFixture testFixture) : IClassFixture<MiBffTestFixture>
{
    private readonly MiBffTestFixture _testFixture = testFixture;

    [Fact]
    public async Task GivenValidUser_WhenDownloadCattleRegistrationsReportRequested_ShouldReturnReport()
    {
        //_testFixture.Factory.OverrideService(); // todo - insert fake 
        var endpoint = "/api/v1/bff/mi/reports/cattle_registrations";
        var request = new GetMonthlyReportRequest { Year = 2026, Month = 4 };
        var payload = HttpContentUtility.CreateApplicationJsonAsStringContent(request);

        var response = await _testFixture.HttpClient.PostAsync(endpoint, payload, TestContext.Current.CancellationToken);
        response.IsSuccessStatusCode.Should().BeTrue(); // todo better assertion
    }
}