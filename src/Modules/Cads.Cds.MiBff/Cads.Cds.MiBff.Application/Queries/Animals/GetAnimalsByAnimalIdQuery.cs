using Cads.Cds.BuildingBlocks.Application.Queries;
using Cads.Cds.MiBff.Core.DTOs;

namespace Cads.Cds.MiBff.Application.Queries.Animals;

public record GetAnimalsByAnimalIdQuery(string AnimalId) : IDefaultQuery<UkvDto>;