using Cads.Cds.BuildingBlocks.Application.Files.Abstractions;

namespace Cads.Cds.BuildingBlocks.Infrastructure.Files.Helpers;

public class PhysicalFileSystem : IFileSystem
{
    public bool Exists(string path) => File.Exists(path);

    public Task<byte[]> ReadAllBytesAsync(string path, CancellationToken cancellationToken) =>
        File.ReadAllBytesAsync(path, cancellationToken);
}