using Cads.Cds.BuildingBlocks.Core.Exceptions;
using Cads.Cds.MiBff.Application.Reports.Queries.GetReportJobStatus;
using Cads.Cds.MiBff.Core.Domain.Entities;
using Cads.Cds.MiBff.Core.Domain.Repositories;
using FluentAssertions;

namespace Cads.Cds.MiBff.Application.Tests.Unit.Reports.Queries.GetReportJobStatus;

public class GetReportJobStatusQueryHandlerTests
{
    [Fact]
    public async Task Handle_WithExistingPendingJob_ReturnsStatusAndNotReady()
    {
        var jobId = Guid.NewGuid();
        var repository = new FakeMiReportJobRepository(new MiReportJob
        {
            ReportJobId = jobId,
            ReportKey = "report_a",
            ExternalSubject = "user@example.test",
            RequestPayload = "{}",
            Status = ReportJobStatus.Pending,
            CreatedAt = DateTimeOffset.UtcNow,
            UpdatedAt = DateTimeOffset.UtcNow
        });

        var handler = new GetReportJobStatusQueryHandler(repository);

        var result = await handler.Handle(new GetReportJobStatusQuery { ReportJobId = jobId }, TestContext.Current.CancellationToken);

        result.JobId.Should().Be(jobId);
        result.Status.Should().Be(ReportJobStatus.Pending);
        result.IsReady.Should().BeFalse();
        result.Error.Should().BeNull();
    }

    [Fact]
    public async Task Handle_WithCompleteJob_ReturnsReady()
    {
        var jobId = Guid.NewGuid();
        var repository = new FakeMiReportJobRepository(new MiReportJob
        {
            ReportJobId = jobId,
            ReportKey = "report_a",
            ExternalSubject = "user@example.test",
            RequestPayload = "{}",
            Status = ReportJobStatus.Complete,
            CreatedAt = DateTimeOffset.UtcNow,
            UpdatedAt = DateTimeOffset.UtcNow
        });

        var handler = new GetReportJobStatusQueryHandler(repository);

        var result = await handler.Handle(new GetReportJobStatusQuery { ReportJobId = jobId }, TestContext.Current.CancellationToken);

        result.Status.Should().Be(ReportJobStatus.Complete);
        result.IsReady.Should().BeTrue();
    }

    [Fact]
    public async Task Handle_WithUnknownJob_ThrowsNotFoundException()
    {
        var repository = new FakeMiReportJobRepository(null);
        var handler = new GetReportJobStatusQueryHandler(repository);

        var act = () => handler.Handle(new GetReportJobStatusQuery { ReportJobId = Guid.NewGuid() }, TestContext.Current.CancellationToken);

        await act.Should().ThrowAsync<NotFoundException>();
    }

    private sealed class FakeMiReportJobRepository(MiReportJob? job) : IMiReportJobRepository
    {
        public Task CreateJobWithOutboxAsync(MiReportJob reportJob, MiReportJobOutbox outboxMessage, CancellationToken cancellationToken = default)
            => throw new NotImplementedException();

        public Task<MiReportJob?> GetByReportJobIdAsync(Guid reportJobId, CancellationToken cancellationToken = default)
            => Task.FromResult(job);

        public Task UpdateStatusAsync(
            Guid reportJobId,
            string status,
            string? s3Key = null,
            int? rowCount = null,
            string? error = null,
            DateTimeOffset? startedAt = null,
            DateTimeOffset? completedAt = null,
            DateTimeOffset? expiresAt = null,
            CancellationToken cancellationToken = default)
            => throw new NotImplementedException();

        public Task<IReadOnlyList<MiReportJobOutbox>> GetPendingOutboxMessagesAsync(int batchSize = 50, CancellationToken cancellationToken = default)
            => throw new NotImplementedException();

        public Task<IReadOnlyList<MiReportJobOutbox>> ClaimPendingOutboxMessagesAsync(int batchSize = 20, CancellationToken cancellationToken = default)
            => throw new NotImplementedException();

        public Task MarkOutboxDispatchFailedAsync(Guid outboxId, string error, int maxAttempts, CancellationToken cancellationToken = default)
            => throw new NotImplementedException();

        public Task DeleteDispatchedOutboxMessagesAsync(IEnumerable<Guid> outboxIds, CancellationToken cancellationToken = default)
            => throw new NotImplementedException();
    }
}