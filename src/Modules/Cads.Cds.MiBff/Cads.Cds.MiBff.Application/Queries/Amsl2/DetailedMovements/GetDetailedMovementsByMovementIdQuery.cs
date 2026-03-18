using Cads.Cds.BuildingBlocks.Application.Queries.JsonResponses;
using Cads.Cds.MiBff.Core.Domain.DTOs.Amls2;

namespace Cads.Cds.MiBff.Application.Queries.Amsl2.DetailedMovements;

public record GetDetailedMovementsByMovementIdQuery(Guid MovementId) : IJsonResponseDataQuery<Amsl2Dto>;