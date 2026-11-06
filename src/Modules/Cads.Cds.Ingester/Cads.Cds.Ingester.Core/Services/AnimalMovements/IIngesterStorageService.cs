using Cads.Cds.Ingester.Core.DTOs.Common;

namespace Cads.Cds.Ingester.Core.Services.AnimalMovements;

public interface IIngesterStorageService
{
    Task<IngestionDto> WriteAsync(string key, string payload, CancellationToken cancellationToken);

    Task<IngestionDto> WriteAsync(string key, Stream fileStream, CancellationToken cancellationToken);
}