using AutoFixture;
using Cads.Cds.BuildingBlocks.Testing.Support.Utilities.Authorization;
using Cads.Cds.BuildingBlocks.Testing.Support.Utilities.Http;
using Cads.Cds.MiBff.Application.Reports.Requests;
using Cads.Cds.MiBff.Core.Domain.Entities;
using Cads.Cds.MiBff.Core.Domain.Repositories;
using Cads.Cds.MiBff.Testing.Support.Constants;
using Cads.Cds.MiBff.Testing.Support.SpecimenBuilders;
using Cads.Cds.MiBff.Tests.Component.TestFixtures;
using FluentAssertions;
using Moq;
using System.Net;

namespace Cads.Cds.MiBff.Tests.Component.Reports;

public class GetGbCattleRegistrationsTests
{
    private readonly Fixture _fixture;

    private readonly Mock<IMiBirthSummaryRepository> _miBirthSummaryRepositoryMock = new();

    public GetGbCattleRegistrationsTests()
    {
        _fixture = new Fixture();
        _fixture.Customizations.Add(new IgnoreNavigationProperties());
    }

    [Fact]
    public async Task GivenInvalidYear_WhenGetGbCattleRegistrationsRequested_ShouldReturnBadRequest()
    {
        var response = await ExecuteTest(0, 4);

        response.Should().NotBeNull();
        response.StatusCode.Should().Be(HttpStatusCode.BadRequest);
    }

    [Fact]
    public async Task GivenInvalidMonth_WhenGetGbCattleRegistrationsRequested_ShouldReturnBadRequest()
    {
        var response = await ExecuteTest(2026, 0);

        response.Should().NotBeNull();
        response.StatusCode.Should().Be(HttpStatusCode.BadRequest);
    }

    [Fact]
    public async Task GivenValidRequest_AndNoRecordsFound_WhenGetGbCattleRegistrationsRequested_ShouldSucceed()
    {
        _miBirthSummaryRepositoryMock.Setup(x => x.GetBirthSummaryAsync(
            It.IsAny<DateOnly>(),
                It.IsAny<DateOnly>(),
                It.IsAny<CancellationToken>()))
            .ReturnsAsync([]);

        var response = await ExecuteTest(2026, 4);

        response.Should().NotBeNull();
        response.IsSuccessStatusCode.Should().BeTrue();
    }

    [Fact]
    public async Task GivenValidRequest_AndRecordsFound_WhenGetGbCattleRegistrationsRequested_ShouldSucceed()
    {
        var expectedBirthSummaryResult = _fixture.CreateMany<MiBirthSummary>(5)
            .ToList();

        _miBirthSummaryRepositoryMock.Setup(x => x.GetBirthSummaryAsync(
            It.IsAny<DateOnly>(),
                It.IsAny<DateOnly>(),
                It.IsAny<CancellationToken>()))
            .ReturnsAsync(expectedBirthSummaryResult);

        var response = await ExecuteTest(2026, 4);

        response.Should().NotBeNull();
        response.IsSuccessStatusCode.Should().BeTrue();
    }

    private MiBffWebApplicationFactory GetFactory(bool useFakeAuth = true)
    {
        var factory = new MiBffWebApplicationFactory(useFakeAuth: useFakeAuth);

        factory.OverrideServiceAsScoped(_miBirthSummaryRepositoryMock.Object);

        return factory;
    }

    private async Task<HttpResponseMessage?> ExecuteTest(int year, int month)
    {
        var endpoint = $"/api/v1/bff/mi/reports/{TestReportKeyConstants.GbCattleRegistrationsReportKey}";
        var request = new GetGbCattleRegistrationsRequest { Year = year, Month = month };
        var payload = HttpContentUtility.CreateApplicationJsonAsStringContent(request);

        var factory = GetFactory();
        var client = factory.CreateClient();
        client.AddJwt();

        return await client.PostAsync(endpoint, payload, TestContext.Current.CancellationToken);
    }
}