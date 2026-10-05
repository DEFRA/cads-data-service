using Cads.Cds.Api.Core.Domain.Animals;
using Microsoft.AspNetCore.Mvc;

namespace Cads.Cds.Api.Application.Queries.Bovine.AnimalsOnHolding;

public sealed class AnimalsOnHoldingFilters
{
    [FromQuery(Name = "sex")]
    public AnimalSex? Sex { get; init; }

    [FromQuery(Name = "breedCode")]
    public string? BreedCode { get; init; }
}