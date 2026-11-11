using Amazon.SQS;
using Cads.Cds.ApiSurface.Messages;
using Cads.Cds.BuildingBlocks.Application.Messaging.Observers;
using Cads.Cds.BuildingBlocks.Infrastructure.Messaging.Configuration;
using Cads.Cds.BuildingBlocks.Infrastructure.Messaging.Consumers;
using Cads.Cds.BuildingBlocks.Infrastructure.Messaging.Factories;
using Cads.Cds.BuildingBlocks.Infrastructure.Messaging.Services;
using Cads.Cds.StorageBridge.Application.Messaging.Clients;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Logging;
using Microsoft.Extensions.Options;

namespace Cads.Cds.StorageBridge.Infrastructure.Messaging.Consumers;

public class StorageBridgeFifoQueuePoller(
    IServiceScopeFactory scopeFactory,
    IAmazonSQS sqs,
    MessageCommandRegistry<StorageBridgeFifoQueueClient> registry,
    IOptionsMonitor<QueueConsumerOptions> options,
    StorageBridgeFifoQueueClient client,
    IQueueAdminService<StorageBridgeFifoQueueClient> queueAdminService,
    IQueuePollerObserver<MessageType> observer,
    ILogger<StorageBridgeFifoQueuePoller> logger)
        : MediatorSqsQueuePoller<StorageBridgeFifoQueueClient>(scopeFactory, sqs, registry, options, client, queueAdminService, observer, logger);