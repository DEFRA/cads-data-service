using Cads.Cds.BuildingBlocks.Application.Messaging.Clients;

namespace Cads.Cds.StorageBridge.Application.Messaging.Clients;

public class StorageBridgeFifoQueueClient : IQueueClient
{
    public string ClientName => GetType().Name;
}