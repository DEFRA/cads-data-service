namespace Cads.Cds.BuildingBlocks.Core.Storage;

public interface IStorageWriter<T>
{
    Task WriteAsync(string key, string payload);
}