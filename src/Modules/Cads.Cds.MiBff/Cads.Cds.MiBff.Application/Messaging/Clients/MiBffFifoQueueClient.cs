using Cads.Cds.BuildingBlocks.Application.Messaging.Clients;

namespace Cads.Cds.MiBff.Application.Messaging.Clients;

public class MiBffFifoQueueClient : IQueueClient
{
    public string ClientName => GetType().Name;
}

