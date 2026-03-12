using Cads.Cds.BuildingBlocks.Application.Queries;
using Cads.Cds.MiBff.Core.DTOs;

namespace Cads.Cds.MiBff.Application.Queries.Amsl2.AnimalSummary;

public record GetAnimalSummaryByAnimalIdQuery(Guid AnimalId) : IDefaultQuery<Amsl2Dto>;