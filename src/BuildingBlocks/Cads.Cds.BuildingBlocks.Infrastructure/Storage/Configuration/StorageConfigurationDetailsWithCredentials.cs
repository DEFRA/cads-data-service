namespace Cads.Cds.BuildingBlocks.Infrastructure.Storage.Configuration;

public record StorageConfigurationDetailsWithCredentials : StorageConfigurationDetails
{
    public string AccessKeySecretName { get; init; } = string.Empty;
    public string SecretKeySecretName { get; init; } = string.Empty;
}