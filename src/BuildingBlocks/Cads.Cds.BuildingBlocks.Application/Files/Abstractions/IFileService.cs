namespace Cads.Cds.BuildingBlocks.Application.Files.Abstractions;

public interface IFileService
{
    Task<T?> ReadJsonFromFileAndReturnAsModelAsync<T>(string filePath, CancellationToken cancellationToken = default)
        where T : class;
}