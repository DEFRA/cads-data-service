using Cads.Cds.Ingester.Core.Domain.Enums;
using Cads.Cds.Ingester.Core.DTOs.Common;
using Cads.Cds.Ingester.Core.Services.AnimalMovements;
using Microsoft.Extensions.Logging;

namespace Cads.Cds.Ingester.Application.Commands.AnimalMovements.Adapters;

public class AnimalMovementByNationCommandAdapter(IIngesterStorageService storageService, ILogger<AnimalMovementByNationCommandAdapter> logger)
{
    public async Task<IngestionDto> WriteAsync(
        AnimalMovementByNationCommand command,
        CancellationToken cancellationToken = default)
    {
        var key = GetKey(command.Nation);

        if (logger.IsEnabled(LogLevel.Information))
        {
            logger.LogInformation("Writing to storage: {Key}", key);
        }
        var result =
            await storageService.WriteAsync(key, command.Payload!, cancellationToken);
        return result;
    }

    private static string GetKey(Nation nation)
    {
        var nationalDirectory = nation.ToString().ToLower();
        return $"inbound/{nationalDirectory}/incremental/animal-movements/{DateTime.UtcNow:yy-MM-dd-HH-mm-ss-fff}.json";
    }
}