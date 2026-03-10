using Cads.Cds.BuildingBlocks.Application.Queries;
using Cads.Cds.MiBff.Core.DTOs;

namespace Cads.Cds.MiBff.Application.Queries.Amsl2.DetailedMovements;

public record GetDetailedMovementsByMovementIdQuery(Guid MovementId) : IDefaultQuery<Amsl2Dto>;