using Cads.Cds.MiBff.Core.DTOs.Reports;
using Cads.Cds.MiBff.Testing.Support.Constants;
using Cads.Cds.MiBff.Tests.Component.TestFixtures;
using FluentAssertions;
using System.Net.Http.Json;

namespace Cads.Cds.MiBff.Tests.Component.Reports;

public class GetUserReportsTests(MiBffTestFixture testFixture) : IClassFixture<MiBffTestFixture>
{
    private readonly MiBffTestFixture _testFixture = testFixture;

    [Fact]
    public async Task GivenValidRequest_GetUserReports_ShouldSucceed()
    {
        var endpoint = "/api/v1/bff/mi/reports";

        var response = await _testFixture.HttpClient.GetAsync(endpoint, TestContext.Current.CancellationToken);
        response.IsSuccessStatusCode.Should().BeTrue();

        var data = await response.Content.ReadFromJsonAsync<List<ReportDto>>(TestContext.Current.CancellationToken);
        data.Should().NotBeNull().And.HaveCountGreaterThanOrEqualTo(1);
    }

    [Fact]
    public async Task GivenMatchingReportKey_GetUserReportPermissions_ShouldSucceed()
    {
        var reportKey = TestReportKeyConstants.HoldingSummaryReportKey;
        var endpoint = $"/api/v1/bff/mi/reports/{reportKey}/permissions";

        var response = await _testFixture.HttpClient.GetAsync(endpoint, TestContext.Current.CancellationToken);
        response.IsSuccessStatusCode.Should().BeTrue();

        var data = await response.Content.ReadFromJsonAsync<UserReportPermissionsDto>(TestContext.Current.CancellationToken);
        data.Should().NotBeNull();
        data.ReportKey.Should().Be(reportKey);
        data.Permissions.Should().NotBeNull().And.HaveCountGreaterThanOrEqualTo(1);
    }

    [Fact]
    public async Task GivenNonMatchingReportKey_GetUserReportPermissions_ShouldSucceed()
    {
        var reportKey = "fake_report";
        var endpoint = $"/api/v1/bff/mi/reports/{reportKey}/permissions";

        var response = await _testFixture.HttpClient.GetAsync(endpoint, TestContext.Current.CancellationToken);
        response.IsSuccessStatusCode.Should().BeTrue();

        var data = await response.Content.ReadFromJsonAsync<UserReportPermissionsDto>(TestContext.Current.CancellationToken);
        data.Should().NotBeNull();
        data.ReportKey.Should().Be(reportKey);
        data.Permissions.Should().BeEmpty();
    }
}