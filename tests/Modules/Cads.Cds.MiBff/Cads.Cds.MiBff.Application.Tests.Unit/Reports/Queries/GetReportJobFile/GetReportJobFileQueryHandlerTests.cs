using Cads.Cds.BuildingBlocks.Core.Exceptions;
using Cads.Cds.BuildingBlocks.Core.OpenXml;
using Cads.Cds.MiBff.Application.Queries.Reports;
using Cads.Cds.MiBff.Application.Reports.Jobs;
using Cads.Cds.MiBff.Application.Reports.Queries.GetReportJobFile;
using Cads.Cds.MiBff.Application.Reports.Routing.Abstractions;
using Cads.Cds.MiBff.Application.Reports.Storage;
using Cads.Cds.MiBff.Core.Domain.Entities;
using Cads.Cds.MiBff.Core.Domain.Repositories;
using Cads.Cds.MiBff.Core.DTOs.Reports;
using FluentAssertions;
using Moq;
using System.Text;
using System.Text.Json;

namespace Cads.Cds.MiBff.Application.Tests.Unit.Reports.Queries.GetReportJobFile;

public class GetReportJobFileQueryHandlerTests
{
    private readonly Mock<IMiReportJobRepository> _repositoryMock = new();
    private readonly Mock<IReportJobQueryBuilder> _queryBuilderMock = new();
    private readonly Mock<IReportFileStore> _fileStoreMock = new();
    private readonly Mock<IReportKeyModelRegistry> _modelRegistryMock = new();
    private readonly Mock<IOpenXmlReportGenerator> _generatorMock = new();
    private readonly Mock<ICcoeReportPublisher> _ccoeReportPublisherMock = new();

    private readonly Guid _jobId = Guid.NewGuid();
    private const string ReportKey = GetGbCattleDeathsQuery.ExpectedKey;
    private static readonly GetGbCattleDeathsQuery Query = new(ReportKey, 2024);

    public GetReportJobFileQueryHandlerTests()
    {
        _queryBuilderMock.Setup(b => b.Build(It.IsAny<MiReportJob>())).Returns(Query);
    }

    private GetReportJobFileQueryHandler CreateSut() => new(
        _repositoryMock.Object,
        _queryBuilderMock.Object,
        _fileStoreMock.Object,
        _modelRegistryMock.Object,
        _generatorMock.Object,
        _ccoeReportPublisherMock.Object);

    private void SeedJob(string status = ReportJobStatus.Complete, string reportKey = ReportKey) =>
        _repositoryMock
            .Setup(r => r.GetByReportJobIdAsync(_jobId, It.IsAny<CancellationToken>()))
            .ReturnsAsync(new MiReportJob
            {
                ReportJobId = _jobId,
                ReportKey = reportKey,
                ExternalSubject = "user@example.test",
                RequestPayload = "{}",
                Status = status
            });

    private void SeedStoredBytes(byte[] bytes) =>
        _fileStoreMock
            .Setup(s => s.GetAsync(It.IsAny<string>(), It.IsAny<CancellationToken>()))
            .ReturnsAsync(() => new MemoryStream(bytes));

    private void SeedNoModelType() =>
        _modelRegistryMock
            .Setup(r => r.TryGetModelType(It.IsAny<string>(), out It.Ref<Type?>.IsAny))
            .Returns(false);

    private void SeedModelType(Type? modelType) =>
        _modelRegistryMock
            .Setup(r => r.TryGetModelType(ReportKey, out modelType))
            .Returns(true);

    private static byte[] BuildJsonArrayBytes(int count) =>
        Encoding.UTF8.GetBytes(
            JsonSerializer.Serialize(
                Enumerable.Range(1, count).Select(i => new { deathYear = i }),
                JsonSerializerOptions.Web));

    [Fact]
    public async Task Handle_WithUnknownJob_ThrowsNotFound()
    {
        _repositoryMock
            .Setup(r => r.GetByReportJobIdAsync(_jobId, It.IsAny<CancellationToken>()))
            .ReturnsAsync((MiReportJob?)null);

        var act = () => CreateSut().Handle(
            new GetReportJobFileQuery { ReportKey = ReportKey, JobId = _jobId }, TestContext.Current.CancellationToken);

        await act.Should().ThrowAsync<NotFoundException>();
    }

    [Fact]
    public async Task Handle_WithMismatchedReportKey_ThrowsNotFound()
    {
        SeedJob(reportKey: "some_other_report");

        var act = () => CreateSut().Handle(
            new GetReportJobFileQuery { ReportKey = ReportKey, JobId = _jobId }, TestContext.Current.CancellationToken);

        await act.Should().ThrowAsync<NotFoundException>();
    }

    [Theory]
    [InlineData(ReportJobStatus.Pending)]
    [InlineData(ReportJobStatus.Processing)]
    [InlineData(ReportJobStatus.Failed)]
    public async Task Handle_WithIncompleteJob_ThrowsNonRetryable(string status)
    {
        SeedJob(status: status);

        var act = () => CreateSut().Handle(
            new GetReportJobFileQuery { ReportKey = ReportKey, JobId = _jobId }, TestContext.Current.CancellationToken);

        await act.Should().ThrowAsync<NonRetryableException>();
    }

    [Fact]
    public async Task Handle_NoModelTypeThrowsNonRetryableException()
    {
        SeedJob();
        SeedNoModelType();
        SeedStoredBytes([1, 2, 3]);

        var act = () => CreateSut().Handle(
            new GetReportJobFileQuery { ReportKey = ReportKey, JobId = _jobId, FileType = ReportFileType.Xlsx },
            TestContext.Current.CancellationToken);

        await act.Should().ThrowAsync<NonRetryableException>();
    }

    [Fact]
    public async Task Handle_WithJsonFileTypeAndModelTypeRegistered_ReturnsDeserialisedPayload()
    {
        SeedJob();
        SeedModelType(typeof(MiDeathSummary));
        SeedStoredBytes(Encoding.UTF8.GetBytes("""[{"deathYear":2024}]"""));

        var result = await CreateSut().Handle(
            new GetReportJobFileQuery { ReportKey = ReportKey, JobId = _jobId, FileType = ReportFileType.Json },
            TestContext.Current.CancellationToken);

        var payload = result.Should().BeOfType<JsonReportResult>().Subject.Payload
            .Should().BeOfType<PagedReportPayload>().Subject;

        payload.Page.Should().Be(1);
        payload.PageSize.Should().Be(GetReportJobFileQueryHandler.JsonPageSize);
        payload.HasMore.Should().BeFalse();
        payload.Items.Should().ContainSingle();
        payload.Items[0].GetProperty("deathYear").GetInt32().Should().Be(2024);
    }

    [Fact]
    public async Task Handle_WithJsonFileTypeDefaultPage_ReturnsFirstPageWithHasMoreTrue_WhenMoreThanPageSizeRows()
    {
        SeedJob();
        SeedModelType(typeof(MiDeathSummary));
        SeedStoredBytes(BuildJsonArrayBytes(GetReportJobFileQueryHandler.JsonPageSize + 1));

        var result = await CreateSut().Handle(
            new GetReportJobFileQuery { ReportKey = ReportKey, JobId = _jobId, FileType = ReportFileType.Json },
            TestContext.Current.CancellationToken);

        var payload = result.Should().BeOfType<JsonReportResult>().Subject.Payload
            .Should().BeOfType<PagedReportPayload>().Subject;

        payload.Page.Should().Be(1);
        payload.PageSize.Should().Be(GetReportJobFileQueryHandler.JsonPageSize);
        payload.HasMore.Should().BeTrue();
        payload.Items.Should().HaveCount(GetReportJobFileQueryHandler.JsonPageSize);
        payload.Items[0].GetProperty("deathYear").GetInt32().Should().Be(1);
        payload.Items[^1].GetProperty("deathYear").GetInt32().Should().Be(GetReportJobFileQueryHandler.JsonPageSize);
    }

    [Fact]
    public async Task Handle_WithJsonFileTypeExactlyPageSizeRows_ReturnsHasMoreFalse()
    {
        SeedJob();
        SeedModelType(typeof(MiDeathSummary));
        SeedStoredBytes(BuildJsonArrayBytes(GetReportJobFileQueryHandler.JsonPageSize));

        var result = await CreateSut().Handle(
            new GetReportJobFileQuery { ReportKey = ReportKey, JobId = _jobId, FileType = ReportFileType.Json },
            TestContext.Current.CancellationToken);

        var payload = result.Should().BeOfType<JsonReportResult>().Subject.Payload
            .Should().BeOfType<PagedReportPayload>().Subject;

        payload.HasMore.Should().BeFalse();
        payload.Items.Should().HaveCount(GetReportJobFileQueryHandler.JsonPageSize);
    }

    [Fact]
    public async Task Handle_WithJsonFileTypeSecondPage_ReturnsNextSliceInStoredOrder()
    {
        SeedJob();
        SeedModelType(typeof(MiDeathSummary));
        var pageSize = GetReportJobFileQueryHandler.JsonPageSize;
        SeedStoredBytes(BuildJsonArrayBytes(pageSize + 10));

        var result = await CreateSut().Handle(
            new GetReportJobFileQuery { ReportKey = ReportKey, JobId = _jobId, FileType = ReportFileType.Json, Page = 2 },
            TestContext.Current.CancellationToken);

        var payload = result.Should().BeOfType<JsonReportResult>().Subject.Payload
            .Should().BeOfType<PagedReportPayload>().Subject;

        payload.Page.Should().Be(2);
        payload.HasMore.Should().BeFalse();
        payload.Items.Should().HaveCount(10);
        payload.Items[0].GetProperty("deathYear").GetInt32().Should().Be(pageSize + 1);
        payload.Items[^1].GetProperty("deathYear").GetInt32().Should().Be(pageSize + 10);
    }

    [Fact]
    public async Task Handle_WithJsonFileTypePageBeyondAvailableRows_ReturnsEmptyItems()
    {
        SeedJob();
        SeedModelType(typeof(MiDeathSummary));
        SeedStoredBytes(BuildJsonArrayBytes(5));

        var result = await CreateSut().Handle(
            new GetReportJobFileQuery { ReportKey = ReportKey, JobId = _jobId, FileType = ReportFileType.Json, Page = 2 },
            TestContext.Current.CancellationToken);

        var payload = result.Should().BeOfType<JsonReportResult>().Subject.Payload
            .Should().BeOfType<PagedReportPayload>().Subject;

        payload.Items.Should().BeEmpty();
        payload.HasMore.Should().BeFalse();
    }

    [Fact]
    public async Task Handle_WithJsonFileTypeAndPageLessThanOne_TreatsAsFirstPage()
    {
        SeedJob();
        SeedModelType(typeof(MiDeathSummary));
        SeedStoredBytes(BuildJsonArrayBytes(3));

        var result = await CreateSut().Handle(
            new GetReportJobFileQuery { ReportKey = ReportKey, JobId = _jobId, FileType = ReportFileType.Json, Page = 0 },
            TestContext.Current.CancellationToken);

        var payload = result.Should().BeOfType<JsonReportResult>().Subject.Payload
            .Should().BeOfType<PagedReportPayload>().Subject;

        payload.Page.Should().Be(1);
        payload.Items.Should().HaveCount(3);
    }

    [Fact]
    public async Task Handle_WithJsonFileTypeAndNoModelTypeRegistered_ThrowsNonRetryable()
    {
        SeedJob();
        SeedNoModelType();
        SeedStoredBytes([1, 2, 3]);

        var act = () => CreateSut().Handle(
            new GetReportJobFileQuery { ReportKey = ReportKey, JobId = _jobId, FileType = ReportFileType.Json },
            TestContext.Current.CancellationToken);

        await act.Should().ThrowAsync<NonRetryableException>();
    }

    [Fact]
    public async Task Handle_WithXlsxFileTypeAndModelTypeRegistered_ConvertsUsingRegisteredModelType()
    {
        SeedJob();
        SeedModelType(typeof(MiDeathSummary));
        SeedStoredBytes(Encoding.UTF8.GetBytes("""[{"deathYear":2024}]"""));

        var xlsxBytes = new MemoryStream([4, 5, 6]);
        _generatorMock
            .Setup(g => g.Generate(It.IsAny<List<MiDeathSummary>>()))
            .Returns(xlsxBytes);

        var expectedLink = new ReportDownloadLink("https://ccoe.example.test/signed", DateTimeOffset.UtcNow.AddMinutes(15));
        _ccoeReportPublisherMock
            .Setup(p => p.PublishXlsxAsync(ReportKey, _jobId, xlsxBytes, It.IsAny<CancellationToken>()))
            .ReturnsAsync(expectedLink);

        var result = await CreateSut().Handle(
            new GetReportJobFileQuery { ReportKey = ReportKey, JobId = _jobId, FileType = ReportFileType.Xlsx },
            TestContext.Current.CancellationToken);

        var redirect = result.Should().BeOfType<RedirectReportResult>().Subject;
        redirect.Url.Should().Be(expectedLink.Url);
        redirect.ExpiresAt.Should().Be(expectedLink.ExpiresAt);
        _generatorMock.Verify(g => g.Generate(It.Is<List<MiDeathSummary>>(d => d.Count == 1 && d[0].DeathYear == 2024)), Times.Once);
        _ccoeReportPublisherMock.Verify(
            p => p.PublishXlsxAsync(ReportKey, _jobId, xlsxBytes, It.IsAny<CancellationToken>()), Times.Once);
    }
}