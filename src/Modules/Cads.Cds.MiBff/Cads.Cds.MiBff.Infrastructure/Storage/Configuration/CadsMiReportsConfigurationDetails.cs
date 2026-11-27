using Cads.Cds.BuildingBlocks.Infrastructure.Storage.Configuration;

namespace Cads.Cds.MiBff.Infrastructure.Storage.Configuration;

public record CadsMiReportsConfigurationDetails : StorageConfigurationDetailsWithCredentials
{
    /// <summary>
    /// How long presigned download URLs remain valid for.
    /// </summary>
    public int PresignedUrlExpiryMinutes { get; init; } = 15;
}