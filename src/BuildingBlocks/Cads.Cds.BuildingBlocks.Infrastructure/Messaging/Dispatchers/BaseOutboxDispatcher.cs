using Cads.Cds.BuildingBlocks.Application.Messaging.Clients;
using Cads.Cds.BuildingBlocks.Application.Messaging.Models;
using Cads.Cds.BuildingBlocks.Application.Messaging.Publishers;
using Cads.Cds.BuildingBlocks.Core.Configuration;
using Cads.Cds.BuildingBlocks.Core.Domain.Outbox;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Hosting;
using Microsoft.Extensions.Logging;
using Microsoft.Extensions.Options;

namespace Cads.Cds.BuildingBlocks.Infrastructure.Messaging.Dispatchers;

/// <summary>
/// Generic polling outbox dispatcher: periodically claims pending rows from <typeparamref name="TRepository"/>,
/// publishes them via <see cref="IMessagePublisher{TClient}"/>, marks per-row publish failures and deletes
/// successfully dispatched rows. Modules only need to supply <see cref="BuildMessage"/> and
/// <see cref="BuildMetadata"/>.
/// </summary>
public abstract class BaseOutboxDispatcher<TClient, TRepository, TOutbox, TMessage>(
    IServiceScopeFactory scopeFactory,
    IMessagePublisher<TClient> publisher,
    IOptionsMonitor<OutboxDispatcherOptions> options,
    ILogger logger)
    : BackgroundService, IOutboxDispatcher<TClient>
    where TClient : IQueueClient, new()
    where TRepository : class, IOutboxRepository<TOutbox>
    where TOutbox : IOutboxMessage
    where TMessage : class
{
    protected readonly IServiceScopeFactory ScopeFactory = scopeFactory;
    protected readonly IMessagePublisher<TClient> Publisher = publisher;
    protected readonly IOptionsMonitor<OutboxDispatcherOptions> Options = options;
    protected readonly ILogger Logger = logger;

    protected override async Task ExecuteAsync(CancellationToken stoppingToken)
    {
        if (stoppingToken.IsCancellationRequested)
            return;

        using var timer = new PeriodicTimer(ToInterval(Options.CurrentValue));

        try
        {
            do
            {
                var currentOptions = Options.CurrentValue;

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
                    Logger.LogError(ex, "Unhandled error while dispatching outbox messages for {Client}.", typeof(TClient).Name);
                }
            }
            while (await timer.WaitForNextTickAsync(stoppingToken));
        }
        catch (OperationCanceledException) when (stoppingToken.IsCancellationRequested)
        {
            // Graceful shutdown.
        }
    }

    protected virtual async Task DispatchPendingAsync(OutboxDispatcherOptions currentOptions, CancellationToken cancellationToken)
    {
        using var scope = ScopeFactory.CreateScope();
        var repository = scope.ServiceProvider.GetRequiredService<TRepository>();

        var claimed = await repository.ClaimPendingOutboxMessagesAsync(currentOptions.BatchSize, cancellationToken);

        if (claimed.Count == 0)
            return;

        var dispatchedOutboxIds = new List<Guid>(claimed.Count);

        foreach (var outboxMessage in claimed)
        {
            try
            {
                var message = BuildMessage(outboxMessage);
                var metadata = BuildMetadata(outboxMessage);

                await Publisher.PublishAsync(message, metadata, cancellationToken);

                dispatchedOutboxIds.Add(outboxMessage.OutboxId);

                if (Logger.IsEnabled(LogLevel.Information))
                {
                    Logger.LogInformation(
                        "Dispatched outbox message {OutboxId} for {Client}.",
                        outboxMessage.OutboxId,
                        typeof(TClient).Name);
                }
            }
            catch (OperationCanceledException) when (cancellationToken.IsCancellationRequested)
            {
                throw;
            }
            catch (Exception ex)
            {
                Logger.LogError(
                    ex,
                    "Failed to dispatch outbox message {OutboxId} for {Client}.",
                    outboxMessage.OutboxId,
                    typeof(TClient).Name);

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

    /// <summary>
    /// Builds the message payload to publish for a claimed outbox row.
    /// </summary>
    protected abstract TMessage BuildMessage(TOutbox outboxMessage);

    /// <summary>
    /// Builds the FIFO publish metadata (group/dedup/correlation ids) for a claimed outbox row.
    /// </summary>
    protected abstract FifoMessageMetadata BuildMetadata(TOutbox outboxMessage);

    private static TimeSpan ToInterval(OutboxDispatcherOptions o) =>
        TimeSpan.FromSeconds(Math.Max(1, o.PollIntervalSeconds));
}


