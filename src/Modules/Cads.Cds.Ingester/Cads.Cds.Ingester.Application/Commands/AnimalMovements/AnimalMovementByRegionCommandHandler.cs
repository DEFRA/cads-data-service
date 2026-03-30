using Cads.Cds.Ingester.Application.Commands.AnimalMovements.Adapters;
using Cads.Cds.Ingester.Core.DTOs.Common;
using MediatR;

namespace Cads.Cds.Ingester.Application.Commands.AnimalMovements;

public class AnimalMovementByRegionCommandHandler(AnimalMovementByRegionCommandAdapter adapter) : IRequestHandler<AnimalMovementByRegionCommand, IngestionDTO>
{

    public async Task<IngestionDTO> Handle(AnimalMovementByRegionCommand request, CancellationToken cancellationToken)
    {
        var result = await adapter.WriteAsync(request, cancellationToken);
        return result;
    }
}