namespace Cads.Cds.BuildingBlocks.Core;

public interface IStorageWriter<T>
{
    Task WriteAsync(string key, string payload);
}