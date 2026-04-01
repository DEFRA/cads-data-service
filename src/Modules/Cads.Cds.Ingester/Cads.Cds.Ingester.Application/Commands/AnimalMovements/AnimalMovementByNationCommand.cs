using Cads.Cds.BuildingBlocks.Application.Commands;
using Cads.Cds.Ingester.Core.Domain.Enums;
using Cads.Cds.Ingester.Core.DTOs.Common;

namespace Cads.Cds.Ingester.Application.Commands.AnimalMovements;

public record AnimalMovementByNationCommand(Nation Nation, string? Payload) : ICommand<IngestionDTO>
{
}