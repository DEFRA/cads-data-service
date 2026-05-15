using System.Net;
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

namespace Cads.Cds.MiBff.Tests.Component.Reports;

public class GetGbCattleImportsTests
{
    private readonly Fixture _fixture;
    private static Mock<IMiImportSummaryRepository> CreateRepositoryMock() => new();

    private static string Endpoint =>
        $"/api/v1/bff/mi/reports/{TestReportKeyConstants.GbCattleImportsReportKey}";

    public GetGbCattleImportsTests()
    {
        _fixture = new Fixture();
        _fixture.Customizations.Add(new IgnoreNavigationProperties());
    }

    [Fact]
    public async Task GivenInvalidYear_WhenGetGbCattleImportsRequested_ShouldReturnBadRequest()
    {
        var response = await ExecuteTest(CreateRepositoryMock(), 0);

        response.Should().NotBeNull();
        response.StatusCode.Should().Be(HttpStatusCode.BadRequest);
    }

    [Fact]
    public async Task GivenValidRequest_AndNoRecordsFound_WhenGetGbCattleImportsRequested_ShouldSucceed()
    {
        var _miImportSummaryRepositoryMock = CreateRepositoryMock();
        _miImportSummaryRepositoryMock.Setup(x => x.GetImportSummaryAsync(
                It.IsAny<DateOnly>(),
                It.IsAny<DateOnly>(),
                It.IsAny<CancellationToken>()))
            .ReturnsAsync([]);

        var response = await ExecuteTest(_miImportSummaryRepositoryMock, 2000);

        response.Should().NotBeNull();
        response.IsSuccessStatusCode.Should().BeTrue();
    }

    [Fact]
    public async Task GivenValidRequest_AndSingleRecordFoundNullable_WhenGetGbCattleImportsRequested_ShouldSucceed()
    {
        var expectedImportSummaryResult = _fixture.Build<MiImportSummary>()
            .With(x => x.Sex, () => null)
            .Create();

        var _miImportSummaryRepositoryMock = CreateRepositoryMock();
        _miImportSummaryRepositoryMock.Setup(x => x.GetImportSummaryAsync(
                It.IsAny<DateOnly>(),
                It.IsAny<DateOnly>(),
                It.IsAny<CancellationToken>()))
            .ReturnsAsync([expectedImportSummaryResult]);

        var response = await ExecuteTest(_miImportSummaryRepositoryMock, 2001);

        response.Should().NotBeNull();
        response.IsSuccessStatusCode.Should().BeTrue();
    }

    [Fact]
    public async Task GivenValidRequest_AndRecordsFound_WhenGetGbCattleImportsRequested_ShouldSucceed()
    {
        var expectedImportSummaryResult = _fixture.CreateMany<MiImportSummary>(5).ToList();

        var _miImportSummaryRepositoryMock = CreateRepositoryMock();
        _miImportSummaryRepositoryMock.Setup(x => x.GetImportSummaryAsync(
                It.IsAny<DateOnly>(),
                It.IsAny<DateOnly>(),
                It.IsAny<CancellationToken>()))
            .ReturnsAsync(expectedImportSummaryResult);

        var response = await ExecuteTest(_miImportSummaryRepositoryMock, 2026);

        response.Should().NotBeNull();
        response.IsSuccessStatusCode.Should().BeTrue();
    }

    private static MiBffWebApplicationFactory GetFactory(Mock<IMiImportSummaryRepository> repositoryMock, bool useFakeAuth = true)
    {
        var factory = new MiBffWebApplicationFactory(useFakeAuth: useFakeAuth);

        factory.OverrideServiceAsScoped(repositoryMock.Object);

        return factory;
    }

    private static async Task<HttpResponseMessage?> ExecuteTest(Mock<IMiImportSummaryRepository> repositoryMock, int year)
    {
        var request = new GetGbCattleImportsRequest { Year = year };
        var payload = HttpContentUtility.CreateApplicationJsonAsStringContent(request);

        await using var factory = GetFactory(repositoryMock);
        using var client = factory.CreateClient();
        client.AddJwt();

        return await client.PostAsync(Endpoint, payload, TestContext.Current.CancellationToken);
    }
}