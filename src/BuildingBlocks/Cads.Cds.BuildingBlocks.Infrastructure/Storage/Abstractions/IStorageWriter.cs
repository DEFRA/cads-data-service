namespace Cads.Cds.BuildingBlocks.Infrastructure.Storage.Abstractions;

public interface IStorageWriter<T>
{
    Task WriteAsync(string key, string payload);
}