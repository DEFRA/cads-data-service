using Cads.Cds.BuildingBlocks.Application.Queries;
using Cads.Cds.MiBff.Core.DTOs;

namespace Cads.Cds.MiBff.Application.Queries.Cohorts;

public record GetCohortsByAnimalIdQuery(string AnimalId) : IDefaultQuery<UkvDto>;