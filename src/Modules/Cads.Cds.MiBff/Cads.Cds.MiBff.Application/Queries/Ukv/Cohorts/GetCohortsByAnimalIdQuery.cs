using Cads.Cds.BuildingBlocks.Application.Queries;
using Cads.Cds.MiBff.Core.DTOs;
using FluentValidation;

namespace Cads.Cds.MiBff.Application.Queries.Ukv.Cohorts;

public record GetCohortsByAnimalIdQuery(Guid AnimalId) : IDefaultQuery<UkvDto>;