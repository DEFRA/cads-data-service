using Cads.Cds.ApiSurface.Messages.Reports;
using Cads.Cds.BuildingBlocks.Application.Messaging.Models;
using Cads.Cds.BuildingBlocks.Application.Messaging.Publishers;
using Cads.Cds.BuildingBlocks.Core.Configuration;
using Cads.Cds.BuildingBlocks.Core.Correlation;
using Cads.Cds.BuildingBlocks.Infrastructure.Messaging.Dispatchers;
using Cads.Cds.MiBff.Application.Messaging.Clients;
using Cads.Cds.MiBff.Core.Domain.Entities;
using Cads.Cds.MiBff.Core.Domain.Repositories;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Logging;
using Microsoft.Extensions.Options;
using System.Text.Json;

namespace Cads.Cds.MiBff.Infrastructure.Messaging.Dispatchers;

/// <summary>
/// Dispatches pending mi_report_job_outbox rows onto the MiBff FIFO queue. All polling/claim/
/// publish/retry logic lives in BaseOutboxDispatcher - this class only knows how to turn an
/// MiReportJobOutbox row into a ReportJobRequestedMessage + FifoMessageMetadata.
/// </summary>
public class MiReportJobOutboxDispatcher(
    IServiceScopeFactory scopeFactory,
    IMessagePublisher<MiBffFifoQueueClient> publisher,
    IOptionsMonitor<OutboxDispatcherOptions> options,
    ILogger<MiReportJobOutboxDispatcher> logger)
    : BaseOutboxDispatcher<MiBffFifoQueueClient, IMiReportJobRepository, MiReportJobOutbox, ReportJobRequestedMessage>(
        scopeFactory, publisher, options, logger)
{
    protected override ReportJobRequestedMessage BuildMessage(MiReportJobOutbox outboxMessage)
    {
        var jobId = outboxMessage.ReportJobId;
        var reportKey = string.Empty;

        try
        {
            using var document = JsonDocument.Parse(outboxMessage.Payload);
            var root = document.RootElement;

            if (root.TryGetProperty("reportJobId", out var jobIdProperty) && jobIdProperty.TryGetGuid(out var parsedJobId))
            {
                jobId = parsedJobId;
            }

            if (root.TryGetProperty("reportKey", out var reportKeyProperty))
            {
                reportKey = reportKeyProperty.GetString() ?? string.Empty;
            }
        }
        catch (JsonException)
        {
            // Ignore if payload couldn't be parsed. The message still carries a valid JobId for processing.
        }

        return new ReportJobRequestedMessage
        {
            Id = outboxMessage.OutboxId,
            JobId = jobId,
            ReportKey = reportKey
        };
    }

    protected override FifoMessageMetadata BuildMetadata(MiReportJobOutbox outboxMessage) =>
        new(
            messageGroupId: outboxMessage.ReportJobId.ToString(),
            messageDeduplicationId: outboxMessage.OutboxId.ToString(),
            correlationId: CorrelationIdContext.Value ?? Guid.NewGuid().ToString());
}

