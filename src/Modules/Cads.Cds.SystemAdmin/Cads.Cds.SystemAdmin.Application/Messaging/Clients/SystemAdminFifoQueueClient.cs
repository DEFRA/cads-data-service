using Cads.Cds.BuildingBlocks.Application.Messaging.Clients;

namespace Cads.Cds.SystemAdmin.Application.Messaging.Clients;

public class SystemAdminFifoQueueClient : IQueueClient
{
    public string ClientName => GetType().Name;
}