using Cads.Cds.BuildingBlocks.Infrastructure.Storage.Abstractions;

namespace Cads.Cds.MiBff.Infrastructure.Storage.Clients;

public class CadsMiReportStorageClient : IStorageClient
{
    public string ClientName => GetType().Name;
}