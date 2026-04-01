using Cads.Cds.Ingester.Application.Commands.AnimalMovements.Adapters;
using Cads.Cds.Ingester.Core.DTOs.Common;
using MediatR;

namespace Cads.Cds.Ingester.Application.Commands.AnimalMovements;

public class AnimalMovementByNationCommandHandler(AnimalMovementByNationCommandAdapter adapter) : IRequestHandler<AnimalMovementByNationCommand, IngestionDto>
{

    public async Task<IngestionDto> Handle(AnimalMovementByNationCommand request, CancellationToken cancellationToken)
    {
        var result = await adapter.WriteAsync(request, cancellationToken);
        return result;
    }
}