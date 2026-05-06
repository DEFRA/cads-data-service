using Cads.Cds.BuildingBlocks.Testing.Support.Utilities.Http;
using Cads.Cds.MiBff.Application.Reports.Requests;
using Cads.Cds.MiBff.Testing.Support.Constants;
using Cads.Cds.MiBff.Tests.Component.TestFixtures;
using FluentAssertions;

namespace Cads.Cds.MiBff.Tests.Component.Reports;

public class GetGbCattleRegistrationsTests(MiBffTestFixture testFixture) : IClassFixture<MiBffTestFixture>
{
    private readonly MiBffTestFixture _testFixture = testFixture;

    [Fact]
    public async Task GivenValidUser_WhenGetGbCattleRegistrationsRequested_ShouldReturnReport()
    {
        var endpoint = $"/api/v1/bff/mi/reports/{TestReportKeyConstants.GbCattleRegistrationsReportKey}";
        var request = new GetGbCattleRegistrationsRequest { Year = 2026, Month = 4 };
        var payload = HttpContentUtility.CreateApplicationJsonAsStringContent(request);

        var response = await _testFixture.HttpClient.PostAsync(endpoint, payload, TestContext.Current.CancellationToken);
        response.IsSuccessStatusCode.Should().BeTrue();
    }
}