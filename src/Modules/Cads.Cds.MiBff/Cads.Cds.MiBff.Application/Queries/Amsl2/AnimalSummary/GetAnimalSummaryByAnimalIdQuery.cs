using Cads.Cds.BuildingBlocks.Application.Queries.JsonResponses;
using Cads.Cds.MiBff.Core.Domain.DTOs.Amls2;

namespace Cads.Cds.MiBff.Application.Queries.Amsl2.AnimalSummary;

public record GetAnimalSummaryByAnimalIdQuery(Guid AnimalId) : IJsonResponseDataQuery<Amsl2Dto>;