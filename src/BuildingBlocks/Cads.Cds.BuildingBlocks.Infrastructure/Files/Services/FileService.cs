using Cads.Cds.BuildingBlocks.Application.Files.Abstractions;
using Cads.Cds.BuildingBlocks.Infrastructure.Json;
using System.Text.Json;

namespace Cads.Cds.BuildingBlocks.Infrastructure.Files.Services;

public class FileService(IFileSystem fileSystem) : IFileService
{
    public async Task<T?> ReadJsonFromFileAndReturnAsModelAsync<T>(string filePath, CancellationToken cancellationToken = default)
        where T : class
    {
        byte[] fileData;

        if (!fileSystem.Exists(filePath))
        {
            throw new FileNotFoundException($"File not found: {filePath}");
        }

        try
        {
            fileData = await fileSystem.ReadAllBytesAsync(filePath, cancellationToken);
        }
        catch (Exception ex)
        {
            throw new IOException($"Failed to read file '{filePath}'.", ex);
        }

        try
        {
            return JsonSerializer.Deserialize<T>(
                fileData,
                JsonDefaults.DefaultOptionsWithIndented
            );
        }
        catch (Exception ex)
        {
            throw new FileLoadException($"Failed to deserialize JSON from '{filePath}'.", ex);
        }
    }
}