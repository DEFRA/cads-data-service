using Cads.Cds.BuildingBlocks.Infrastructure.Storage.Configuration;

namespace Cads.Cds.StorageBridge.Infrastructure.Storage.Configuration;

public record StorageBridgeStorageConfiguration
{
    public StorageConfigurationDetails CadsInternal { get; init; } = new();

    public StorageConfigurationDetailsWithCredentials CadsExternal { get; init; } = new();
}