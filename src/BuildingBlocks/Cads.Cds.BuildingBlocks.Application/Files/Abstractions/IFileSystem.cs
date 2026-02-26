namespace Cads.Cds.BuildingBlocks.Application.Files.Abstractions;

public interface IFileSystem
{
    bool Exists(string path);
    Task<byte[]> ReadAllBytesAsync(string path, CancellationToken cancellationToken);
}