using Cads.Cds.ApiSurface.Messages.Reports;
using Cads.Cds.BuildingBlocks.Infrastructure.Messaging.Dispatchers;
using Cads.Cds.BuildingBlocks.Testing.Support.TestDoubles.Publishers;
using Cads.Cds.MiBff.Application.Messaging.Clients;
using Cads.Cds.MiBff.Core.Domain.Entities;
using Cads.Cds.MiBff.Testing.Support.Fakes.Repositories;
using Cads.Cds.MiBff.Tests.Component.TestFixtures;
using FluentAssertions;
using Microsoft.Extensions.DependencyInjection;

namespace Cads.Cds.MiBff.Tests.Component.Dispatchers;

public class MiReportJobOutboxDispatcherTests(MiBffTestFixture testFixture) : IClassFixture<MiBffTestFixture>
{
    private readonly MiBffTestFixture _testFixture = testFixture;

    [Fact]
    public async Task GivenPendingOutboxMessage_WhenDispatcherRuns_ThenPublishesMessageAndDeletesOutboxRow()
    {
        await using var scope = _testFixture.Factory.Services.CreateAsyncScope();

        var repository = scope.ServiceProvider.GetRequiredService<FakeMiReportJobRepository>();
        var publisherSpy = scope.ServiceProvider.GetRequiredService<TestMessagePublisher<MiBffFifoQueueClient>>();
        var dispatcher = scope.ServiceProvider.GetRequiredService<IOutboxDispatcher<MiBffFifoQueueClient>>();

        // The test doubles are singletons (see OverrideOutboxDispatcher), so reset any state
        // left over from a previous test sharing the same MiBffWebApplicationFactory instance.
        publisherSpy.Reset();

        var reportJobId = Guid.NewGuid();
        var outboxId = Guid.NewGuid();

        repository.SeedOutbox(new MiReportJobOutbox
        {
            OutboxId = outboxId,
            ReportJobId = reportJobId,
            Subject = "report.job.requested",
            Payload = $$"""{"reportJobId":"{{reportJobId}}","reportKey":"holdings_summary"}""",
            Status = ReportJobOutboxStatus.Dispatching,
            CreatedAt = DateTimeOffset.UtcNow
        });

        using var cts = new CancellationTokenSource();

        try
        {
            await dispatcher.StartAsync(cts.Token);

            var (message, metadata) = await publisherSpy.Published.WaitAsync(TimeSpan.FromSeconds(10), TestContext.Current.CancellationToken);

            var typedMessage = message.Should().BeOfType<ReportJobRequestedMessage>().Subject;
            typedMessage.Id.Should().Be(outboxId);
            typedMessage.JobId.Should().Be(reportJobId);
            typedMessage.ReportKey.Should().Be("holdings_summary");

            metadata.MessageGroupId.Should().Be(reportJobId.ToString());
            metadata.MessageDeduplicationId.Should().Be(outboxId.ToString());

            await WaitUntilAsync(() => repository.DeletedOutboxIds.Contains(outboxId));
            repository.FailedCalls.Should().NotContain(c => c.OutboxId == outboxId);
        }
        finally
        {
            await dispatcher.StopAsync(CancellationToken.None);
        }
    }

    [Fact]
    public async Task GivenPublishThrows_WhenDispatcherRuns_ThenMarksOutboxFailedAndDoesNotDeleteIt()
    {
        await using var scope = _testFixture.Factory.Services.CreateAsyncScope();

        var repository = scope.ServiceProvider.GetRequiredService<FakeMiReportJobRepository>();
        var publisherSpy = scope.ServiceProvider.GetRequiredService<TestMessagePublisher<MiBffFifoQueueClient>>();
        var dispatcher = scope.ServiceProvider.GetRequiredService<IOutboxDispatcher<MiBffFifoQueueClient>>();

        publisherSpy.Reset();
        publisherSpy.ThrowOnNextPublish(new InvalidOperationException("SQS unavailable"));

        var outboxId = Guid.NewGuid();

        repository.SeedOutbox(new MiReportJobOutbox
        {
            OutboxId = outboxId,
            ReportJobId = Guid.NewGuid(),
            Subject = "report.job.requested",
            Payload = "{}",
            Status = ReportJobOutboxStatus.Dispatching,
            CreatedAt = DateTimeOffset.UtcNow
        });

        using var cts = new CancellationTokenSource();

        try
        {
            await dispatcher.StartAsync(cts.Token);

            await publisherSpy.PublishAttempted.WaitAsync(TimeSpan.FromSeconds(10), TestContext.Current.CancellationToken);

            await WaitUntilAsync(() => repository.FailedCalls.Any(c => c.OutboxId == outboxId));

            repository.FailedCalls.Should().ContainSingle(c =>
                c.OutboxId == outboxId && c.Error == "SQS unavailable");
            repository.DeletedOutboxIds.Should().NotContain(outboxId);
        }
        finally
        {
            await dispatcher.StopAsync(CancellationToken.None);
        }
    }

    private static async Task WaitUntilAsync(Func<bool> predicate, int timeoutMs = 5000)
    {
        var deadline = DateTime.UtcNow.AddMilliseconds(timeoutMs);

        while (!predicate() && DateTime.UtcNow < deadline)
        {
            await Task.Delay(25, TestContext.Current.CancellationToken);
        }

        predicate().Should().BeTrue("the expected repository state should be reached before the timeout");
    }
}