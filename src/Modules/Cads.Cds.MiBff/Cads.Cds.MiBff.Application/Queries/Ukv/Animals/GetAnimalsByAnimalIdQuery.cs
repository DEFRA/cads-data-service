using Cads.Cds.BuildingBlocks.Application.Queries;
using Cads.Cds.MiBff.Core.DTOs;

namespace Cads.Cds.MiBff.Application.Queries.Ukv.Animals;

public record GetAnimalsByAnimalIdQuery(Guid AnimalId) : IDefaultQuery<UkvDto>;