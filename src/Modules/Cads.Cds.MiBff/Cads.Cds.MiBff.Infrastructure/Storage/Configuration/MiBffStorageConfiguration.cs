using Cads.Cds.BuildingBlocks.Infrastructure.Storage.Configuration;

namespace Cads.Cds.MiBff.Infrastructure.Storage.Configuration;

public class MiBffStorageConfiguration
{
    public StorageConfigurationDetails CadsInternal { get; init; } = new();
}
