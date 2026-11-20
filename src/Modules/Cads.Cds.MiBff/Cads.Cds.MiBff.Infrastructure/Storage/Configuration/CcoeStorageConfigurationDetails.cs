using Cads.Cds.BuildingBlocks.Infrastructure.Storage.Configuration;

namespace Cads.Cds.MiBff.Infrastructure.Storage.Configuration;

public record CcoeStorageConfigurationDetails : StorageConfigurationDetails
{
    public string KmsKeyArn { get; init; } = string.Empty;

    /// <summary>
    /// How long presigned download URLs remain valid for.
    /// </summary>
    public int PresignedUrlExpiryMinutes { get; init; } = 15;
}