using Cads.Cds.BuildingBlocks.Application.Queries.JsonResponses;
using Cads.Cds.MiBff.Core.DTOs.Ukv;

namespace Cads.Cds.MiBff.Application.Queries.Ukv.Animals;

public record GetAnimalsByAnimalIdQuery(Guid AnimalId) : IJsonResponseDataQuery<UkvDto>;