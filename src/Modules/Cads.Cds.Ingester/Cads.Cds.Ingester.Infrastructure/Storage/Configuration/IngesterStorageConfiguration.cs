using Cads.Cds.BuildingBlocks.Infrastructure.Storage.Configuration;

namespace Cads.Cds.Ingester.Infrastructure.Storage.Configuration;

public class IngesterStorageConfiguration
{
    public StorageConfigurationDetails CadsIngester { get; init; } = new();
}