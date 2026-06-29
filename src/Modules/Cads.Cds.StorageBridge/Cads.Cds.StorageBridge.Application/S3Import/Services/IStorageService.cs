using Cads.Cds.BuildingBlocks.Infrastructure.Storage.Abstractions;

namespace Cads.Cds.StorageBridge.Application.S3Import.Services;

public interface IStorageService<T> : IStorageReader<T>, IStorageWriter<T> where T : IStorageClient, new()
{
}