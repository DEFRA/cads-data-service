using Cads.Cds.ApiSurface.Messages.Reports;
using Cads.Cds.BuildingBlocks.Application.Messaging.Models;
using Cads.Cds.BuildingBlocks.Core.Correlation;
using Cads.Cds.MiBff.Core.Domain.Entities;
using Cads.Cds.MiBff.Infrastructure.Messaging.Dispatchers;
using FluentAssertions;
using System.Reflection;

namespace Cads.Cds.MiBff.Infrastructure.Tests.Unit.Messaging.Dispatchers;

/// <summary>
/// All the polling/claim/publish/retry logic is now covered by BaseOutboxDispatcherTests in
/// Cads.Cds.BuildingBlocks.Infrastructure.Tests.Unit. This class only covers the MiBff-specific
/// (private, static) BuildMessage/BuildMetadata mapping from an MiReportJobOutbox row.
/// </summary>
public class MiReportJobOutboxDispatcherTests
{
    private static MiReportJobOutbox CreateOutbox(Guid? reportJobId = null, string payload = "{}")
        => new()
        {
            OutboxId = Guid.NewGuid(),
            ReportJobId = reportJobId ?? Guid.NewGuid(),
            Subject = "report.job.requested",
            Payload = payload,
            Status = ReportJobOutboxStatus.Dispatching,
            CreatedAt = DateTimeOffset.UtcNow
        };

    [Fact]
    public void BuildMessage_WithFullPayload_UsesParsedJobIdAndReportKey()
    {
        var reportJobId = Guid.NewGuid();
        var outbox = CreateOutbox(Guid.NewGuid(), $$"""{"reportJobId":"{{reportJobId}}","reportKey":"gb_cattle_registrations"}""");

        var message = InvokeBuildMessage(outbox);

        message.Id.Should().Be(outbox.OutboxId);
        message.JobId.Should().Be(reportJobId);
        message.ReportKey.Should().Be("gb_cattle_registrations");
    }

    [Fact]
    public void BuildMessage_WithEmptyPayload_FallsBackToOutboxReportJobIdAndEmptyReportKey()
    {
        var outbox = CreateOutbox(payload: "{}");

        var message = InvokeBuildMessage(outbox);

        message.Id.Should().Be(outbox.OutboxId);
        message.JobId.Should().Be(outbox.ReportJobId);
        message.ReportKey.Should().BeEmpty();
    }

    [Fact]
    public void BuildMessage_WithInvalidJsonPayload_FallsBackGracefullyWithoutThrowing()
    {
        var outbox = CreateOutbox(payload: "{not-valid-json");

        var message = InvokeBuildMessage(outbox);

        message.Id.Should().Be(outbox.OutboxId);
        message.JobId.Should().Be(outbox.ReportJobId);
        message.ReportKey.Should().BeEmpty();
    }

    [Fact]
    public void BuildMessage_WithNonGuidJobIdInPayload_FallsBackToOutboxReportJobId()
    {
        var outbox = CreateOutbox(payload: """{"reportJobId":"not-a-guid","reportKey":"holdings_summary"}""");

        var message = InvokeBuildMessage(outbox);

        message.JobId.Should().Be(outbox.ReportJobId);
        message.ReportKey.Should().Be("holdings_summary");
    }

    [Fact]
    public void BuildMetadata_WhenCorrelationIdContextIsSet_UsesItAsCorrelationId()
    {
        var outbox = CreateOutbox();

        CorrelationIdContext.Value = "existing-correlation-id";

        try
        {
            var metadata = InvokeBuildMetadata(outbox);

            metadata.MessageGroupId.Should().Be(outbox.ReportJobId.ToString());
            metadata.MessageDeduplicationId.Should().Be(outbox.OutboxId.ToString());
            metadata.CorrelationId.Should().Be("existing-correlation-id");
        }
        finally
        {
            CorrelationIdContext.Value = null;
        }
    }

    [Fact]
    public void BuildMetadata_WhenCorrelationIdContextIsNotSet_GeneratesANonEmptyCorrelationId()
    {
        var outbox = CreateOutbox();

        CorrelationIdContext.Value = null;

        var metadata = InvokeBuildMetadata(outbox);

        metadata.CorrelationId.Should().NotBeNullOrWhiteSpace();
    }

    private static ReportJobRequestedMessage InvokeBuildMessage(MiReportJobOutbox outbox)
    {
        var method = typeof(MiReportJobOutboxDispatcher)
            .GetMethod("BuildMessage", BindingFlags.NonPublic | BindingFlags.Static);

        return (ReportJobRequestedMessage)method!.Invoke(null, [outbox])!;
    }

    private static FifoMessageMetadata InvokeBuildMetadata(MiReportJobOutbox outbox)
    {
        var method = typeof(MiReportJobOutboxDispatcher)
            .GetMethod("BuildMetadata", BindingFlags.NonPublic | BindingFlags.Static);

        return (FifoMessageMetadata)method!.Invoke(null, [outbox])!;
    }
}