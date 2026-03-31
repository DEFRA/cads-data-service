using Cads.Cds.BuildingBlocks.Infrastructure.Storage.Abstractions;

namespace Cads.Cds.Ingester.Infrastructure.Storage.Clients;

public class IngesterClient : IStorageClient
{
    public string ClientName => GetType().Name;
}