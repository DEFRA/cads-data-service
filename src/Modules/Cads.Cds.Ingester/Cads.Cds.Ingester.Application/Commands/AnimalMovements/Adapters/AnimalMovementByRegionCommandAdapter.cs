using Cads.Cds.Ingester.Core.DTOs.Common;
using Cads.Cds.Ingester.Core.Services.AnimalMovements;

namespace Cads.Cds.Ingester.Application.Commands.AnimalMovements.Adapters;

public class AnimalMovementByRegionCommandAdapter(IIngesterStorageService storageService)
{
    public async Task<IngestionDTO> WriteAsync(
        AnimalMovementByRegionCommand command,
        CancellationToken cancellationToken = default)
    {
        var result =
            await storageService.WriteAsync($"{command.Region}-{DateTime.UtcNow:yy-MM-dd-hh-mm-ss}", command.Payload!);
        return result;
    }
}