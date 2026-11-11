using Cads.Cds.BuildingBlocks.Core.Exceptions;
using Cads.Cds.MiBff.Application.Queries.Reports;
using Cads.Cds.MiBff.Application.Reports.Jobs;
using Cads.Cds.MiBff.Application.Reports.Storage;
using Cads.Cds.MiBff.Core.Domain.Entities;
using Cads.Cds.MiBff.Core.Domain.Repositories;
using Cads.Cds.MiBff.Core.DTOs.Reports;
using FluentAssertions;
using MediatR;
using Microsoft.Extensions.Logging.Abstractions;
using Moq;

namespace Cads.Cds.MiBff.Application.Tests.Unit.Reports.Jobs;

public class ReportJobProcessorTests
{
    private readonly Mock<IMiReportJobRepository> _repositoryMock = new();
    private readonly Mock<IReportJobQueryBuilder> _queryBuilderMock = new();
    private readonly Mock<IMediator> _mediatorMock = new();
    private readonly Mock<IReportFileStore> _fileStoreMock = new();

    private readonly Guid _jobId = Guid.NewGuid();
    private readonly GetGbCattleDeathsQuery _query = new(GetGbCattleDeathsQuery.ExpectedKey, 2024);

    private string ExpectedKey => $"mibff-reports/gbcattledeaths/{_jobId}";

    public ReportJobProcessorTests()
    {
        _queryBuilderMock.Setup(b => b.Build(It.IsAny<MiReportJob>())).Returns(_query);
    }

    private ReportJobProcessor CreateSut() => new(
        _repositoryMock.Object,
        _queryBuilderMock.Object,
        _mediatorMock.Object,
        _fileStoreMock.Object,
        NullLogger<ReportJobProcessor>.Instance);

    private void SeedJob(string status = ReportJobStatus.Pending) =>
        _repositoryMock
            .Setup(r => r.GetByReportJobIdAsync(_jobId, It.IsAny<CancellationToken>()))
            .ReturnsAsync(new MiReportJob
            {
                ReportJobId = _jobId,
                ReportKey = GetGbCattleDeathsQuery.ExpectedKey,
                ExternalSubject = "user@example.test",
                RequestPayload = "{}",
                Status = status
            });

    private void SetupQueryResult(object? result) =>
        _mediatorMock
            .Setup(m => m.Send(It.IsAny<object>(), It.IsAny<CancellationToken>()))
            .ReturnsAsync(result);

    private static FileReportResult NewFile(byte[]? bytes = null) =>
        new(new MemoryStream(bytes ?? [1, 2, 3]), "f.json", "application/json");

    private void VerifyStatus(string status, Times times) =>
        _repositoryMock.Verify(r => r.UpdateStatusAsync(
            _jobId, status,
            It.IsAny<string?>(), It.IsAny<int?>(), It.IsAny<string?>(),
            It.IsAny<DateTimeOffset?>(), It.IsAny<DateTimeOffset?>(), It.IsAny<DateTimeOffset?>(),
            It.IsAny<CancellationToken>()), times);

    private void VerifyNoStatusUpdates() =>
        _repositoryMock.Verify(r => r.UpdateStatusAsync(
            It.IsAny<Guid>(), It.IsAny<string>(),
            It.IsAny<string?>(), It.IsAny<int?>(), It.IsAny<string?>(),
            It.IsAny<DateTimeOffset?>(), It.IsAny<DateTimeOffset?>(), It.IsAny<DateTimeOffset?>(),
            It.IsAny<CancellationToken>()), Times.Never);

    [Theory]
    [InlineData(ReportJobStatus.Pending)]
    [InlineData(ReportJobStatus.Processing)]
    public async Task ProcessAsync_WithRunnableJob_SendsBuiltQuery(string status)
    {
        SeedJob(status);
        SetupQueryResult(NewFile());

        await CreateSut().ProcessAsync(_jobId, TestContext.Current.CancellationToken);

        _mediatorMock.Verify(m => m.Send((object)_query, It.IsAny<CancellationToken>()), Times.Once);
    }

    [Fact]
    public async Task ProcessAsync_WithSuccessfulReport_StoresOutputUnderReportKey()
    {
        SeedJob();
        byte[]? stored = null;
        _fileStoreMock
            .Setup(s => s.SaveAsync(It.IsAny<string>(), It.IsAny<Stream>(), It.IsAny<CancellationToken>()))
            .Callback((string _, Stream content, CancellationToken _) => stored = ((MemoryStream)content).ToArray())
            .Returns(Task.CompletedTask);
        SetupQueryResult(NewFile([9, 8, 7]));

        await CreateSut().ProcessAsync(_jobId, TestContext.Current.CancellationToken);

        _fileStoreMock.Verify(s => s.SaveAsync(ExpectedKey, It.IsAny<Stream>(), It.IsAny<CancellationToken>()), Times.Once);
        stored.Should().Equal(9, 8, 7);
    }

    [Fact]
    public async Task ProcessAsync_WithSuccessfulReport_MarksProcessingThenCompleteWithStorageKey()
    {
        SeedJob();
        SetupQueryResult(NewFile());

        await CreateSut().ProcessAsync(_jobId, TestContext.Current.CancellationToken);

        VerifyStatus(ReportJobStatus.Processing, Times.Once());
        _repositoryMock.Verify(r => r.UpdateStatusAsync(
            _jobId, ReportJobStatus.Complete,
            ExpectedKey, It.IsAny<int?>(), It.IsAny<string?>(),
            It.IsAny<DateTimeOffset?>(), It.IsNotNull<DateTimeOffset?>(), It.IsAny<DateTimeOffset?>(),
            It.IsAny<CancellationToken>()), Times.Once);
        VerifyStatus(ReportJobStatus.Failed, Times.Never());
    }

    [Fact]
    public async Task ProcessAsync_WithSuccessfulReport_DisposesReportStream()
    {
        SeedJob();
        var file = NewFile();
        SetupQueryResult(file);

        await CreateSut().ProcessAsync(_jobId, TestContext.Current.CancellationToken);

        file.Stream.CanRead.Should().BeFalse();
    }

    [Theory]
    [InlineData(ReportJobStatus.Complete)]
    [InlineData(ReportJobStatus.Failed)]
    [InlineData(ReportJobStatus.Expired)]
    public async Task ProcessAsync_WithFinalisedJob_SkipsWithoutRunningReport(string status)
    {
        SeedJob(status);

        await CreateSut().ProcessAsync(_jobId, TestContext.Current.CancellationToken);

        _queryBuilderMock.Verify(b => b.Build(It.IsAny<MiReportJob>()), Times.Never);
        _mediatorMock.Verify(m => m.Send(It.IsAny<object>(), It.IsAny<CancellationToken>()), Times.Never);
        _fileStoreMock.Verify(s => s.SaveAsync(It.IsAny<string>(), It.IsAny<Stream>(), It.IsAny<CancellationToken>()), Times.Never);
        VerifyNoStatusUpdates();
    }

    [Fact]
    public async Task ProcessAsync_WhenJobNotFound_ThrowsNonRetryable()
    {
        _repositoryMock
            .Setup(r => r.GetByReportJobIdAsync(_jobId, It.IsAny<CancellationToken>()))
            .ReturnsAsync((MiReportJob?)null);

        var act = () => CreateSut().ProcessAsync(_jobId, TestContext.Current.CancellationToken);

        await act.Should().ThrowAsync<NonRetryableException>().WithMessage($"*{_jobId}*not found*");
        VerifyNoStatusUpdates();
    }

    [Fact]
    public async Task ProcessAsync_WhenQueryCannotBeBuilt_MarksFailedWithoutProcessing()
    {
        SeedJob();
        _queryBuilderMock.Setup(b => b.Build(It.IsAny<MiReportJob>())).Throws(new NonRetryableException("not supported"));

        var act = () => CreateSut().ProcessAsync(_jobId, TestContext.Current.CancellationToken);

        await act.Should().ThrowAsync<NonRetryableException>().WithMessage("not supported");
        VerifyStatus(ReportJobStatus.Processing, Times.Never());
        VerifyStatus(ReportJobStatus.Failed, Times.Once());
        _mediatorMock.Verify(m => m.Send(It.IsAny<object>(), It.IsAny<CancellationToken>()), Times.Never);
    }

    [Fact]
    public async Task ProcessAsync_WhenQueryReturnsNonFileResult_MarksFailedAndThrowsNonRetryable()
    {
        SeedJob();
        SetupQueryResult(new JsonReportResult(new { }));

        var act = () => CreateSut().ProcessAsync(_jobId, TestContext.Current.CancellationToken);

        await act.Should().ThrowAsync<NonRetryableException>().WithMessage("*expected FileReportResult*");
        VerifyStatus(ReportJobStatus.Failed, Times.Once());
        VerifyStatus(ReportJobStatus.Complete, Times.Never());
        _fileStoreMock.Verify(s => s.SaveAsync(It.IsAny<string>(), It.IsAny<Stream>(), It.IsAny<CancellationToken>()), Times.Never);
    }

    [Fact]
    public async Task ProcessAsync_WhenQueryThrowsUnexpectedException_MarksFailedWithMessageAndRethrows()
    {
        SeedJob();
        _mediatorMock
            .Setup(m => m.Send(It.IsAny<object>(), It.IsAny<CancellationToken>()))
            .ThrowsAsync(new InvalidOperationException("boom"));

        var act = () => CreateSut().ProcessAsync(_jobId, TestContext.Current.CancellationToken);

        await act.Should().ThrowAsync<InvalidOperationException>().WithMessage("boom");
        _repositoryMock.Verify(r => r.UpdateStatusAsync(
            _jobId, ReportJobStatus.Failed,
            It.IsAny<string?>(), It.IsAny<int?>(), "boom",
            It.IsAny<DateTimeOffset?>(), It.IsAny<DateTimeOffset?>(), It.IsAny<DateTimeOffset?>(),
            CancellationToken.None), Times.Once);
    }

    [Fact]
    public async Task ProcessAsync_WhenStorageFailsTransiently_LeavesJobProcessingAndRethrows()
    {
        SeedJob();
        SetupQueryResult(NewFile());
        _fileStoreMock
            .Setup(s => s.SaveAsync(It.IsAny<string>(), It.IsAny<Stream>(), It.IsAny<CancellationToken>()))
            .ThrowsAsync(new RetryableException("s3 unavailable"));

        var act = () => CreateSut().ProcessAsync(_jobId, TestContext.Current.CancellationToken);

        await act.Should().ThrowAsync<RetryableException>();
        VerifyStatus(ReportJobStatus.Processing, Times.Once());
        VerifyStatus(ReportJobStatus.Failed, Times.Never());
        VerifyStatus(ReportJobStatus.Complete, Times.Never());
    }

    [Fact]
    public async Task ProcessAsync_WhenCancelled_DoesNotMarkFailed()
    {
        SeedJob();
        _mediatorMock
            .Setup(m => m.Send(It.IsAny<object>(), It.IsAny<CancellationToken>()))
            .ThrowsAsync(new OperationCanceledException());

        var act = () => CreateSut().ProcessAsync(_jobId, TestContext.Current.CancellationToken);

        await act.Should().ThrowAsync<OperationCanceledException>();
        VerifyStatus(ReportJobStatus.Failed, Times.Never());
    }
}