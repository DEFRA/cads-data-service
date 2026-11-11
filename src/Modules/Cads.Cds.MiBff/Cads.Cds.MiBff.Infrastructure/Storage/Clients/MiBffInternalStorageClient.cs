using Cads.Cds.BuildingBlocks.Infrastructure.Storage.Abstractions;

namespace Cads.Cds.MiBff.Infrastructure.Storage.Clients;

/// <summary>S3 client for the CADS internal bucket, used to store generated MI reports.</summary>
public class MiBffInternalStorageClient : IStorageClient
{
    public string ClientName => GetType().Name;
}