using Cads.Cds.BuildingBlocks.Infrastructure.Persistence.Factories;
using Cads.Cds.MiBff.Core.Domain.Entities;
using Cads.Cds.MiBff.Infrastructure.Persistence.Contexts;
using Cads.Cds.MiBff.Infrastructure.Persistence.Repositories;
using Cads.Cds.MiBff.Testing.Support.Fakes.Uow;
using Microsoft.EntityFrameworkCore;

namespace Cads.Cds.MiBff.Infrastructure.Tests.Unit.Repositories.Mi;

public class MiReportJobRepositoryTests : IDisposable
{
    private readonly MiBffWriteDbContext _context;
    private readonly MiReportJobRepository _repository;

    public MiReportJobRepositoryTests()
    {
        // unique in-memory database name per test instance to isolate data
        _context = DbContextFactory.CreateInMemoryDbContext<MiBffWriteDbContext>(Guid.NewGuid().ToString());
        var uow = new FakeMiBffUnitOfWork(_context);
        _repository = new MiReportJobRepository(_context, uow);
    }

    [Fact]
    public async Task CreateJobWithOutbox_WithValidEntities_PersistsBothRows()
    {
        var reportJob = CreateReportJob(out var reportJobId);
        var outboxMessage = CreateOutboxMessage(reportJobId, out var outboxId);

        await _repository.CreateJobWithOutboxAsync(reportJob, outboxMessage, TestContext.Current.CancellationToken);

        var persistedJob = await _context.ReportJobs
            .SingleOrDefaultAsync(j => j.ReportJobId == reportJobId, TestContext.Current.CancellationToken);
        var persistedOutbox = await _context.ReportJobOutboxMessages
            .SingleOrDefaultAsync(o => o.OutboxId == outboxId, TestContext.Current.CancellationToken);

        Assert.NotNull(persistedJob);
        Assert.NotNull(persistedOutbox);
        Assert.Equal(reportJobId, persistedOutbox.ReportJobId);
    }

    [Fact]
    public async Task CreateJobWithOutbox_WithNullReportJob_ThrowsException()
    {
        var outboxMessage = CreateOutboxMessage(Guid.NewGuid(), out _);

        await Assert.ThrowsAsync<ArgumentNullException>(() =>
            _repository.CreateJobWithOutboxAsync(null!, outboxMessage, TestContext.Current.CancellationToken));
    }

    [Fact]
    public async Task CreateJobWithOutbox_WithNullOutboxMessage_ThrowsException()
    {
        var reportJob = CreateReportJob(out _);

        await Assert.ThrowsAsync<ArgumentNullException>(() =>
            _repository.CreateJobWithOutboxAsync(reportJob, null!, TestContext.Current.CancellationToken));
    }

    [Fact]
    public async Task GetByReportJobId_Found_ReturnsItem()
    {
        var reportJob = CreateReportJob(out var reportJobId);
        var outboxMessage = CreateOutboxMessage(reportJobId, out _);
        await _repository.CreateJobWithOutboxAsync(reportJob, outboxMessage, TestContext.Current.CancellationToken);

        var result = await _repository.GetByReportJobIdAsync(reportJobId, TestContext.Current.CancellationToken);

        Assert.NotNull(result);
        Assert.Equal(reportJobId, result.ReportJobId);
    }

    [Fact]
    public async Task GetByReportJobId_NotFound_ReturnsNull()
    {
        var result = await _repository.GetByReportJobIdAsync(Guid.NewGuid(), TestContext.Current.CancellationToken);

        Assert.Null(result);
    }

    [Fact]
    public async Task UpdateStatus_WithExistingJob_UpdatesFieldsAndTimestamp()
    {
        var reportJob = CreateReportJob(out var reportJobId);
        var outboxMessage = CreateOutboxMessage(reportJobId, out _);
        await _repository.CreateJobWithOutboxAsync(reportJob, outboxMessage, TestContext.Current.CancellationToken);

        var originalUpdatedAt = reportJob.UpdatedAt;

        await _repository.UpdateStatusAsync(
            reportJobId,
            ReportJobStatus.Complete,
            s3Key: "reports/job.csv",
            rowCount: 10,
            completedAt: DateTimeOffset.UtcNow,
            cancellationToken: TestContext.Current.CancellationToken);

        var updated = await _context.ReportJobs
            .SingleAsync(j => j.ReportJobId == reportJobId, TestContext.Current.CancellationToken);

        Assert.Equal(ReportJobStatus.Complete, updated.Status);
        Assert.Equal("reports/job.csv", updated.S3Key);
        Assert.Equal(10, updated.RowCount);
        Assert.NotNull(updated.CompletedAt);
        Assert.True(updated.UpdatedAt > originalUpdatedAt);
    }

    [Fact]
    public async Task UpdateStatus_WithUnknownJob_ThrowsException()
    {
        await Assert.ThrowsAsync<InvalidOperationException>(() =>
            _repository.UpdateStatusAsync(
                Guid.NewGuid(),
                ReportJobStatus.Failed,
                cancellationToken: TestContext.Current.CancellationToken));
    }

    [Fact]
    public async Task GetPendingOutboxMessages_ReturnsOnlyPendingOrderedByCreatedAt()
    {
        var reportJob = CreateReportJob(out var reportJobId);
        await _context.ReportJobs.AddAsync(reportJob, TestContext.Current.CancellationToken);

        var older = new MiReportJobOutbox
        {
            OutboxId = Guid.NewGuid(),
            ReportJobId = reportJobId,
            Subject = "report.job.created",
            Payload = "{}",
            Status = ReportJobOutboxStatus.Pending,
            CreatedAt = new DateTimeOffset(2026, 1, 1, 0, 0, 0, TimeSpan.Zero)
        };

        var newer = new MiReportJobOutbox
        {
            OutboxId = Guid.NewGuid(),
            ReportJobId = reportJobId,
            Subject = "report.job.created",
            Payload = "{}",
            Status = ReportJobOutboxStatus.Pending,
            CreatedAt = new DateTimeOffset(2026, 1, 2, 0, 0, 0, TimeSpan.Zero)
        };

        var dispatched = new MiReportJobOutbox
        {
            OutboxId = Guid.NewGuid(),
            ReportJobId = reportJobId,
            Subject = "report.job.created",
            Payload = "{}",
            Status = ReportJobOutboxStatus.Dispatched,
            CreatedAt = new DateTimeOffset(2026, 1, 3, 0, 0, 0, TimeSpan.Zero),
            DispatchedAt = DateTimeOffset.UtcNow
        };

        await _context.ReportJobOutboxMessages.AddRangeAsync([older, newer, dispatched]);
        await _context.SaveChangesAsync(TestContext.Current.CancellationToken);

        var result = await _repository.GetPendingOutboxMessagesAsync(cancellationToken: TestContext.Current.CancellationToken);

        Assert.Equal(2, result.Count);
        Assert.Equal(older.OutboxId, result[0].OutboxId);
        Assert.Equal(newer.OutboxId, result[1].OutboxId);
    }

    [Fact]
    public async Task GetPendingOutboxMessages_RespectsBatchSize()
    {
        var reportJob = CreateReportJob(out var reportJobId);
        await _context.ReportJobs.AddAsync(reportJob, TestContext.Current.CancellationToken);

        for (var i = 0; i < 5; i++)
        {
            await _context.ReportJobOutboxMessages.AddAsync(new MiReportJobOutbox
            {
                OutboxId = Guid.NewGuid(),
                ReportJobId = reportJobId,
                Subject = "report.job.created",
                Payload = "{}",
                Status = ReportJobOutboxStatus.Pending,
                CreatedAt = DateTimeOffset.UtcNow.AddMinutes(i)
            }, TestContext.Current.CancellationToken);
        }

        await _context.SaveChangesAsync(TestContext.Current.CancellationToken);

        var result = await _repository.GetPendingOutboxMessagesAsync(batchSize: 2, cancellationToken: TestContext.Current.CancellationToken);

        Assert.Equal(2, result.Count);
    }

    [Fact]
    public async Task DeleteDispatchedOutboxMessages_RemovesOnlySpecifiedRows()
    {
        var reportJob = CreateReportJob(out var reportJobId);
        await _context.ReportJobs.AddAsync(reportJob, TestContext.Current.CancellationToken);

        var toDelete = new MiReportJobOutbox
        {
            OutboxId = Guid.NewGuid(),
            ReportJobId = reportJobId,
            Subject = "report.job.created",
            Payload = "{}",
            Status = ReportJobOutboxStatus.Dispatched,
            CreatedAt = DateTimeOffset.UtcNow,
            DispatchedAt = DateTimeOffset.UtcNow
        };

        var toKeep = new MiReportJobOutbox
        {
            OutboxId = Guid.NewGuid(),
            ReportJobId = reportJobId,
            Subject = "report.job.created",
            Payload = "{}",
            Status = ReportJobOutboxStatus.Pending,
            CreatedAt = DateTimeOffset.UtcNow
        };

        await _context.ReportJobOutboxMessages.AddRangeAsync([toDelete, toKeep]);
        await _context.SaveChangesAsync(TestContext.Current.CancellationToken);

        await _repository.DeleteDispatchedOutboxMessagesAsync([toDelete.OutboxId], TestContext.Current.CancellationToken);

        var remaining = await _context.ReportJobOutboxMessages.ToListAsync(TestContext.Current.CancellationToken);

        Assert.Single(remaining);
        Assert.Equal(toKeep.OutboxId, remaining[0].OutboxId);
    }

    [Fact]
    public async Task DeleteDispatchedOutboxMessages_WithEmptyIds_DoesNothing()
    {
        var reportJob = CreateReportJob(out var reportJobId);
        var outboxMessage = CreateOutboxMessage(reportJobId, out _);
        await _repository.CreateJobWithOutboxAsync(reportJob, outboxMessage, TestContext.Current.CancellationToken);

        await _repository.DeleteDispatchedOutboxMessagesAsync([], TestContext.Current.CancellationToken);

        var remaining = await _context.ReportJobOutboxMessages.ToListAsync(TestContext.Current.CancellationToken);

        Assert.Single(remaining);
    }

    private static MiReportJob CreateReportJob(out Guid reportJobId)
    {
        reportJobId = Guid.NewGuid();

        return new MiReportJob
        {
            ReportJobId = reportJobId,
            ReportKey = "report_a",
            ExternalSubject = "user@example.test",
            RequestPayload = "{}",
            Status = ReportJobStatus.Pending,
            CreatedAt = DateTimeOffset.UtcNow,
            UpdatedAt = DateTimeOffset.UtcNow
        };
    }

    private static MiReportJobOutbox CreateOutboxMessage(Guid reportJobId, out Guid outboxId)
    {
        outboxId = Guid.NewGuid();

        return new MiReportJobOutbox
        {
            OutboxId = outboxId,
            ReportJobId = reportJobId,
            Subject = "report.job.created",
            Payload = "{}",
            Status = ReportJobOutboxStatus.Pending,
            CreatedAt = DateTimeOffset.UtcNow
        };
    }

    public void Dispose()
    {
        _context.Dispose();
        GC.SuppressFinalize(this);
    }
}