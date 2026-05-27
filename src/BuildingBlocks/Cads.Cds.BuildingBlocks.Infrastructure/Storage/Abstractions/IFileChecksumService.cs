namespace Cads.Cds.BuildingBlocks.Infrastructure.Storage.Abstractions;

public interface IFileChecksumService
{
    Task<string> ComputeChecksumAsync(string location, CancellationToken cancellationToken);
}