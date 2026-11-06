using Cads.Cds.BuildingBlocks.Application.Messaging.Clients;
using Cads.Cds.BuildingBlocks.Application.Messaging.Publishers;
using Cads.Cds.BuildingBlocks.Core.Configuration;
using Cads.Cds.BuildingBlocks.Core.Domain.Outbox;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Hosting;
using Microsoft.Extensions.Logging;
using Microsoft.Extensions.Options;

namespace Cads.Cds.BuildingBlocks.Infrastructure.Messaging.Dispatchers;

/// <summary>
/// Generic polling outbox dispatcher: periodically claims pending rows from
/// <see cref="IOutboxRepository{TOutbox}"/> (resolved per-poll from DI), delegates building +
/// publishing the message for each row to <see cref="PublishAsync"/>, and marks per-row publish
/// failures/deletes successfully dispatched rows. Only two type parameters are needed - the queue
/// client and the outbox row type - the message type stays an implementation detail of
/// <see cref="PublishAsync"/> in each module's dispatcher subclass.
/// </summary>
public abstract class BaseOutboxDispatcher<TClient, TOutbox>(
    IServiceScopeFactory scopeFactory,
    IMessagePublisher<TClient> publisher,
    IOptionsMonitor<OutboxDispatcherOptions> options,
    ILogger logger)
    : BackgroundService, IOutboxDispatcher<TClient>
    where TClient : IQueueClient, new()
    where TOutbox : IOutboxMessage
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
        var repository = scope.ServiceProvider.GetRequiredService<IOutboxRepository<TOutbox>>();

        var claimed = await repository.ClaimPendingOutboxMessagesAsync(currentOptions.BatchSize, cancellationToken);

        if (claimed.Count == 0)
            return;

        var dispatchedOutboxIds = new List<Guid>(claimed.Count);

        foreach (var outboxMessage in claimed)
        {
            try
            {
                await PublishAsync(outboxMessage, cancellationToken);

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
    /// Builds the message + FIFO metadata for a claimed outbox row and publishes it via
    /// <see cref="Publisher"/>. Keeping this as a single method (rather than separate
    /// BuildMessage/BuildMetadata abstract members) avoids needing the message type as a third
    /// generic parameter on the dispatcher - implementations simply call
    /// <c>Publisher.PublishAsync(message, metadata, cancellationToken)</c> with their own
    /// strongly-typed message.
    /// </summary>
    protected abstract Task PublishAsync(TOutbox outboxMessage, CancellationToken cancellationToken);

    private static TimeSpan ToInterval(OutboxDispatcherOptions o) =>
        TimeSpan.FromSeconds(Math.Max(1, o.PollIntervalSeconds));
}