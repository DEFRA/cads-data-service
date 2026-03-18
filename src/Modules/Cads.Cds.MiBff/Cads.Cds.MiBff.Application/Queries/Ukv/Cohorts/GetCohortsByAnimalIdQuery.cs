using Cads.Cds.BuildingBlocks.Application.Queries.JsonResponses;
using Cads.Cds.MiBff.Core.Domain.DTOs.Ukv;

namespace Cads.Cds.MiBff.Application.Queries.Ukv.Cohorts;

public record GetCohortsByAnimalIdQuery(Guid AnimalId) : IJsonResponseDataQuery<UkvDto>;