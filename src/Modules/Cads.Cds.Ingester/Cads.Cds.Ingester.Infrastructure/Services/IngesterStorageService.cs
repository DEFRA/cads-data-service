using Cads.Cds.BuildingBlocks.Core;
using Cads.Cds.Ingester.Core.DTOs.Common;
using Cads.Cds.Ingester.Core.Services.AnimalMovements;
using Cads.Cds.Ingester.Infrastructure.Storage.Clients;

namespace Cads.Cds.Ingester.Infrastructure.Services;

public class IngesterStorageService(IStorageWriter<CadsIngesterClient> storageWriter) : IIngesterStorageService
{
    public async Task<IngestionDTO> WriteAsync(string key, string payload)
    {
        await storageWriter.WriteAsync(key, payload);

        return new IngestionDTO
        {
            IngestionId = key,
            ReceivedAt = DateTimeOffset.UtcNow,
            RecordCount = 1
        };
    }
}