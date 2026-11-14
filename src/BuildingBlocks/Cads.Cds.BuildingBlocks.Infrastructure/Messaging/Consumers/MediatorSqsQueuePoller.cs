using Amazon.SQS;
using Cads.Cds.ApiSurface.Messages;
using Cads.Cds.BuildingBlocks.Application.Messaging.Clients;
using Cads.Cds.BuildingBlocks.Application.Messaging.Models;
using Cads.Cds.BuildingBlocks.Application.Messaging.Observers;
using Cads.Cds.BuildingBlocks.Infrastructure.Messaging.Configuration;
using Cads.Cds.BuildingBlocks.Infrastructure.Messaging.Factories;
using Cads.Cds.BuildingBlocks.Infrastructure.Messaging.Services;
using MediatR;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Logging;
using Microsoft.Extensions.Options;

namespace Cads.Cds.BuildingBlocks.Infrastructure.Messaging.Consumers;

/// <summary>
/// SQS poller that turns each message into a command via <see cref="MessageCommandRegistry{TClient}"/>
/// and dispatches it through MediatR in a fresh DI scope.
/// </summary>
public abstract class MediatorSqsQueuePoller<TClient>(
    IServiceScopeFactory scopeFactory,
    IAmazonSQS sqs,
    MessageCommandRegistry<TClient> registry,
    IOptionsMonitor<QueueConsumerOptions> options,
    TClient client,
    IQueueAdminService<TClient> queueAdminService,
    IQueuePollerObserver<MessageType> observer,
    ILogger logger)
    : BaseSqsQueuePoller<TClient>(scopeFactory, sqs, options, client, queueAdminService, observer, logger)
    where TClient : IQueueClient
{
    protected override async Task<MessageType?> ProcessMessageAsync(
        UnwrappedMessage message,
        CancellationToken cancellationToken)
    {
        // Async scope: handlers may resolve services that only implement IAsyncDisposable
        // (e.g. unit-of-work types), which throw if the scope is disposed synchronously.
        await using var scope = ScopeFactory.CreateAsyncScope();
        var mediator = scope.ServiceProvider.GetRequiredService<IMediator>();
        var command = registry.CreateCommand(message);
        return await mediator.Send(command, cancellationToken);
    }
}