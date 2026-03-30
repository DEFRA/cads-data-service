using Cads.Cds.Ingester.Core.DTOs.Common;
using Cads.Cds.Ingester.Core.Services.AnimalMovements;

namespace Cads.Cds.Ingester.Application.Commands.AnimalMovements.Adapters;

public class AnimalMovementByRegionCommandAdapter(IIngesterStorageService storageService)
{
    public async Task<IngestionDTO> WriteAsync(
        AnimalMovementByRegionCommand command,
        CancellationToken cancellationToken = default)
    {
        var key = $"inbound/{command.Region.ToString().ToLower()}/incremental/animal-movement-{DateTime.UtcNow:yy-MM-dd-hh-mm-ss}";
        var result =
            await storageService.WriteAsync(key, command.Payload!);
        return result;
    }
}