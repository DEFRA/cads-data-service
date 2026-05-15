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

public class GetGbCattleDeathsTests
{
    private readonly Fixture _fixture;
    private static Mock<IMiDeathSummaryRepository> CreateRepositoryMock() => new();

    private static string Endpoint =>
        $"/api/v1/bff/mi/reports/{TestReportKeyConstants.GbCattleDeathsReportKey}";

    public GetGbCattleDeathsTests()
    {
        _fixture = new Fixture();
        _fixture.Customizations.Add(new IgnoreNavigationProperties());
    }

    [Fact]
    public async Task GivenInvalidYear_WhenGetGbCattleDeathsRequested_ShouldReturnBadRequest()
    {
        var response = await ExecuteTest(CreateRepositoryMock(), 0);

        response.Should().NotBeNull();
        response.StatusCode.Should().Be(HttpStatusCode.BadRequest);
    }

    [Fact]
    public async Task GivenValidRequest_AndNoRecordsFound_WhenGetGbCattleDeathsRequested_ShouldSucceed()
    {
        var _miDeathSummaryRepositoryMock = CreateRepositoryMock();
        _miDeathSummaryRepositoryMock.Setup(x => x.GetDeathSummaryAsync(
                It.IsAny<DateOnly>(),
                It.IsAny<DateOnly>(),
                It.IsAny<CancellationToken>()))
            .ReturnsAsync([]);

        var response = await ExecuteTest(_miDeathSummaryRepositoryMock, 2026);

        response.Should().NotBeNull();
        response.IsSuccessStatusCode.Should().BeTrue();
    }

    [Fact]
    public async Task GivenValidRequest_AndSingleRecordFoundNullable_WhenGetGbCattleDeathsRequested_ShouldSucceed()
    {
        var expectedDeathSummaryResult = _fixture.Build<MiDeathSummary>()
            .With(x => x.Sex, () => null)
            .Create();

        var _miDeathSummaryRepositoryMock = CreateRepositoryMock();
        _miDeathSummaryRepositoryMock.Setup(x => x.GetDeathSummaryAsync(
                It.IsAny<DateOnly>(),
                It.IsAny<DateOnly>(),
                It.IsAny<CancellationToken>()))
            .ReturnsAsync([expectedDeathSummaryResult]);

        var response = await ExecuteTest(_miDeathSummaryRepositoryMock, 2026);

        response.Should().NotBeNull();
        response.IsSuccessStatusCode.Should().BeTrue();
    }

    [Fact]
    public async Task GivenValidRequest_AndRecordsFound_WhenGetGbCattleDeathsRequested_ShouldSucceed()
    {
        var expectedDeathSummaryResult = _fixture.CreateMany<MiDeathSummary>(5).ToList();

        var _miDeathSummaryRepositoryMock = CreateRepositoryMock();
        _miDeathSummaryRepositoryMock.Setup(x => x.GetDeathSummaryAsync(
                It.IsAny<DateOnly>(),
                It.IsAny<DateOnly>(),
                It.IsAny<CancellationToken>()))
            .ReturnsAsync(expectedDeathSummaryResult);

        var response = await ExecuteTest(_miDeathSummaryRepositoryMock, 2026);

        response.Should().NotBeNull();
        response.IsSuccessStatusCode.Should().BeTrue();
    }

    private static MiBffWebApplicationFactory GetFactory(Mock<IMiDeathSummaryRepository> repositoryMock, bool useFakeAuth = true)
    {
        var factory = new MiBffWebApplicationFactory(useFakeAuth: useFakeAuth);

        factory.OverrideServiceAsScoped(repositoryMock.Object);

        return factory;
    }

    private static async Task<HttpResponseMessage?> ExecuteTest(Mock<IMiDeathSummaryRepository> repositoryMock, int year)
    {
        var request = new GetGbCattleDeathsRequest { Year = year };
        var payload = HttpContentUtility.CreateApplicationJsonAsStringContent(request);

        await using var factory = GetFactory(repositoryMock);
        using var client = factory.CreateClient();
        client.AddJwt();

        return await client.PostAsync(Endpoint, payload, TestContext.Current.CancellationToken);
    }
}