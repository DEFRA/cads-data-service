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
        _generatorMock.Object);

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

    private void SeedModelType(Type modelType) =>
        _modelRegistryMock
            .Setup(r => r.TryGetModelType(ReportKey, out modelType))
            .Returns(true);

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
        // var expectedKey = ReportStorageKeys.For(typeof(GetGbCattleDeathsQuery), _jobId);
        // _fileStoreMock.Verify(s => s.GetAsync(expectedKey, It.IsAny<CancellationToken>()), Times.Once);
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

        var json = result.Should().BeOfType<JsonReportResult>().Subject;
        var element = (JsonElement)json.Payload;
        element.ValueKind.Should().Be(JsonValueKind.Array);
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

        var result = await CreateSut().Handle(
            new GetReportJobFileQuery { ReportKey = ReportKey, JobId = _jobId, FileType = ReportFileType.Xlsx },
            TestContext.Current.CancellationToken);

        var file = result.Should().BeOfType<FileReportResult>().Subject;
        file.Stream.Should().BeSameAs(xlsxBytes);
        file.ContentType.Should().Be("application/vnd.openxmlformats-officedocument.spreadsheetml.sheet");
        _generatorMock.Verify(g => g.Generate(It.Is<List<MiDeathSummary>>(d => d.Count == 1 && d[0].DeathYear == 2024)), Times.Once);
    }
}




