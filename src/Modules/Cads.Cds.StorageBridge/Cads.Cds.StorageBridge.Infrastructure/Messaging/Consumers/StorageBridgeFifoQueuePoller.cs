using Amazon.SQS;
using Cads.Cds.BuildingBlocks.Application.Messaging.Messages;
using Cads.Cds.BuildingBlocks.Application.Messaging.Models;
using Cads.Cds.BuildingBlocks.Application.Messaging.Observers;
using Cads.Cds.BuildingBlocks.Infrastructure.Messaging.Configuration;
using Cads.Cds.BuildingBlocks.Infrastructure.Messaging.Consumers;
using Cads.Cds.BuildingBlocks.Infrastructure.Messaging.Services;
using Cads.Cds.StorageBridge.Application.Messaging.Clients;
using Cads.Cds.StorageBridge.Infrastructure.Messaging.Factories;
using MediatR;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Logging;
using Microsoft.Extensions.Options;

namespace Cads.Cds.StorageBridge.Infrastructure.Messaging.Consumers;

public class StorageBridgeFifoQueuePoller(
    IServiceScopeFactory scopeFactory,
    IAmazonSQS sqs,
    StorageBridgeMessageCommandRegistry registry,
    IOptionsMonitor<QueueConsumerOptions> options,
    StorageBridgeFifoQueueClient client,
    IQueueAdminService<StorageBridgeFifoQueueClient> queueAdminService,
    IQueuePollerObserver<MessageType> observer,
    ILogger<StorageBridgeFifoQueuePoller> logger)
        : BaseSqsQueuePoller<StorageBridgeFifoQueueClient>(scopeFactory, sqs, options, client, queueAdminService, observer, logger)
{
    protected override async Task<MessageType?> ProcessMessageAsync(
        UnwrappedMessage message,
        CancellationToken cancellationToken)
    {
        using var scope = ScopeFactory.CreateScope();
        var mediator = scope.ServiceProvider.GetRequiredService<IMediator>();
        var command = registry.CreateCommand(message);
        return await mediator.Send(command, cancellationToken);
    }
}
