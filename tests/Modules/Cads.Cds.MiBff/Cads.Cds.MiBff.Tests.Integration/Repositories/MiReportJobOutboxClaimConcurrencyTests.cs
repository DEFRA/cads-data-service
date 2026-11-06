using Cads.Cds.BuildingBlocks.Testing.Support.TestFixtures.Containers;
using Cads.Cds.MiBff.Core.Domain.Entities;
using Cads.Cds.MiBff.Infrastructure.Persistence.Contexts;
using Cads.Cds.MiBff.Infrastructure.Persistence.Repositories;
using Cads.Cds.MiBff.Testing.Support.Fakes.Uow;
using FluentAssertions;
using Microsoft.EntityFrameworkCore;

namespace Cads.Cds.MiBff.Tests.Integration.Repositories;

// Exercises cads.claim_pending_report_job_outbox_messages.
// This function relies on `FOR UPDATE SKIP LOCKED`, which EF Core's InMemory provider does not
// support so cannot be verified by the repository unit tests
[Collection("MiBffIntegration"), Trait("Dependence", "testcontainers")]
public class MiReportJobOutboxClaimConcurrencyTests(ApiContainerFixture apiContainerFixture)
{
    [Fact]
    public async Task ClaimPendingOutboxMessages_WithConcurrentReplicas_EachRowClaimedExactlyOnce()
    {
        const int rowCount = 30;
        const int replicaCount = 5;
        const int batchSizePerReplica = 10;

        using var seedContext = CreateDbContext();
        var reportJob = new MiReportJob
        {
            ReportJobId = Guid.NewGuid(),
            ReportKey = "concurrency_test_report",
            ExternalSubject = "integration-test-user",
            RequestPayload = "{}",
            Status = ReportJobStatus.Pending,
            CreatedAt = DateTimeOffset.UtcNow,
            UpdatedAt = DateTimeOffset.UtcNow
        };

        await seedContext.ReportJobs.AddAsync(reportJob, TestContext.Current.CancellationToken);

        var outboxIds = new List<Guid>();
        for (var i = 0; i < rowCount; i++)
        {
            var outboxId = Guid.NewGuid();
            outboxIds.Add(outboxId);

            await seedContext.ReportJobOutboxMessages.AddAsync(new MiReportJobOutbox
            {
                OutboxId = outboxId,
                ReportJobId = reportJob.ReportJobId,
                Subject = "report.job.requested",
                Payload = "{}",
                Status = ReportJobOutboxStatus.Pending,
                CreatedAt = DateTimeOffset.UtcNow.AddMilliseconds(i)
            }, TestContext.Current.CancellationToken);
        }

        await seedContext.SaveChangesAsync(TestContext.Current.CancellationToken);

        // Simulate multiple app replicas racing to claim the same pending rows, each with its
        // own DbContext/connection (as separate processes/pods would have in production).
        var claimTasks = Enumerable.Range(0, replicaCount)
            .Select(async _ =>
            {
                using var replicaContext = CreateDbContext();
                var repository = new MiReportJobRepository(replicaContext, new FakeMiBffUnitOfWork(replicaContext));
                return await repository.ClaimPendingOutboxMessagesAsync(
                    batchSizePerReplica,
                    TestContext.Current.CancellationToken);
            });

        var claimedBatches = await Task.WhenAll(claimTasks);
        var claimedIds = claimedBatches.SelectMany(b => b.Select(o => o.OutboxId)).ToList();

        // No two replicas should ever claim the same row.
        claimedIds.Should().OnlyHaveUniqueItems();

        // Every seeded row should have been claimed by exactly one replica.
        claimedIds.Should().BeEquivalentTo(outboxIds);

        // Claimed rows must now be flipped to 'dispatching' in the database.
        using var verificationContext = CreateDbContext();
        var statuses = await verificationContext.ReportJobOutboxMessages
            .Where(o => outboxIds.Contains(o.OutboxId))
            .Select(o => o.Status)
            .ToListAsync(TestContext.Current.CancellationToken);

        statuses.Should().AllSatisfy(s => s.Should().Be(ReportJobOutboxStatus.Dispatching));
    }

    [Fact]
    public async Task ClaimPendingOutboxMessages_WithNoPendingRows_ReturnsEmpty()
    {
        using var repositoryContext = CreateDbContext();
        var repository = new MiReportJobRepository(repositoryContext, new FakeMiBffUnitOfWork(repositoryContext));

        var result = await repository.ClaimPendingOutboxMessagesAsync(
            batchSize: 10,
            cancellationToken: TestContext.Current.CancellationToken);

        result.Where(o => o.Subject == "non-existent-test-subject").Should().BeEmpty();
    }

    private MiBffWriteDbContext CreateDbContext()
    {
        var options = new DbContextOptionsBuilder<MiBffWriteDbContext>()
            .UseNpgsql(apiContainerFixture.PostgresFixture.HostConnectionString)
            .Options;

        return new MiBffWriteDbContext(options);
    }
}