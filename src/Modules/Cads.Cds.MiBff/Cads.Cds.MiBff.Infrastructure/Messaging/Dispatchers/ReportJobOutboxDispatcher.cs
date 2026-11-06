using Cads.Cds.ApiSurface.Messages.Reports;
using Cads.Cds.BuildingBlocks.Application.Messaging.Models;
using Cads.Cds.BuildingBlocks.Application.Messaging.Publishers;
using Cads.Cds.BuildingBlocks.Core.Correlation;
using Cads.Cds.MiBff.Application.Messaging.Clients;
using Cads.Cds.MiBff.Core.Configuration;
using Cads.Cds.MiBff.Core.Domain.Entities;
using Cads.Cds.MiBff.Core.Domain.Repositories;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Hosting;
using Microsoft.Extensions.Logging;
using Microsoft.Extensions.Options;
using System.Text.Json;

namespace Cads.Cds.MiBff.Infrastructure.Messaging.Dispatchers;

public class ReportJobOutboxDispatcher(
    IServiceScopeFactory scopeFactory,
    IMessagePublisher<MiBffFifoQueueClient> publisher,
    IOptionsMonitor<ReportJobOutboxDispatcherOptions> options,
    ILogger<ReportJobOutboxDispatcher> logger)
    : BackgroundService
{
    protected override async Task ExecuteAsync(CancellationToken stoppingToken)
    {
        using var timer = new PeriodicTimer(ToInterval(options.CurrentValue));

        try
        {
            do
            {
                var currentOptions = options.CurrentValue;

                // Pick up changes to PollIntervalSeconds (settable from .NET 8) without
                // waiting for the current tick to elapse.
                var interval = ToInterval(currentOptions);
                if (timer.Period != interval)
                {
                    timer.Period = interval;
                }

                try
                {
                    await DispatchPendingAsync(currentOptions, stoppingToken);
                }
                catch (Exception ex) when (ex is not OperationCanceledException || !stoppingToken.IsCancellationRequested)
                {
                    logger.LogError(ex, "Unhandled error while dispatching mi_report_job_outbox messages.");
                }
            }
            while (await timer.WaitForNextTickAsync(stoppingToken));
        }
        catch (OperationCanceledException) when (stoppingToken.IsCancellationRequested)
        {
            // Graceful shutdown.
        }
    }

    private static TimeSpan ToInterval(ReportJobOutboxDispatcherOptions o) =>
        TimeSpan.FromSeconds(Math.Max(1, o.PollIntervalSeconds));

    private async Task DispatchPendingAsync(ReportJobOutboxDispatcherOptions currentOptions, CancellationToken cancellationToken)
    {
        using var scope = scopeFactory.CreateScope();
        var repository = scope.ServiceProvider.GetRequiredService<IMiReportJobRepository>();

        var claimed = await repository.ClaimPendingOutboxMessagesAsync(currentOptions.BatchSize, cancellationToken);

        if (claimed.Count == 0)
            return;

        var dispatchedOutboxIds = new List<Guid>(claimed.Count);

        foreach (var outboxMessage in claimed)
        {
            try
            {
                var message = BuildMessage(outboxMessage);

                var metadata = new FifoMessageMetadata(
                    messageGroupId: outboxMessage.ReportJobId.ToString(),
                    messageDeduplicationId: outboxMessage.OutboxId.ToString(),
                    correlationId: CorrelationIdContext.Value ?? Guid.NewGuid().ToString());

                await publisher.PublishAsync(message, metadata, cancellationToken);

                dispatchedOutboxIds.Add(outboxMessage.OutboxId);

                if (logger.IsEnabled(LogLevel.Information))
                {
                    logger.LogInformation(
                        "Dispatched report job outbox message {OutboxId} for job {ReportJobId} (subject: {Subject}).",
                        outboxMessage.OutboxId,
                        outboxMessage.ReportJobId,
                        outboxMessage.Subject);
                }
            }
            catch (OperationCanceledException) when (cancellationToken.IsCancellationRequested)
            {
                throw;
            }
            catch (Exception ex)
            {
                logger.LogError(
                    ex,
                    "Failed to dispatch report job outbox message {OutboxId} for job {ReportJobId}.",
                    outboxMessage.OutboxId,
                    outboxMessage.ReportJobId);

                await repository.MarkOutboxDispatchFailedAsync(
                    outboxMessage.OutboxId,
                    ex.Message,
                    currentOptions.MaxAttempts,
                    cancellationToken);
            }
        }

        if (dispatchedOutboxIds.Count > 0)
        {
            await repository.DeleteDispatchedOutboxMessagesAsync(dispatchedOutboxIds, cancellationToken);
        }
    }

    private static ReportJobRequestedMessage BuildMessage(MiReportJobOutbox outboxMessage)
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
}