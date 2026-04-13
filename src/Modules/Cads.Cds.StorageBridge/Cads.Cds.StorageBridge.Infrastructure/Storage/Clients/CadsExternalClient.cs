using Cads.Cds.BuildingBlocks.Infrastructure.Storage.Abstractions;

namespace Cads.Cds.StorageBridge.Infrastructure.Storage.Clients;

public class CadsExternalClient : IStorageClient
{
    public string ClientName => GetType().Name;
}