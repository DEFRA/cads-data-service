namespace Cads.Cds.BuildingBlocks.Core.Storage;

public interface IStorageWriter
{
    Task WriteAsync(string key, string payload);
}