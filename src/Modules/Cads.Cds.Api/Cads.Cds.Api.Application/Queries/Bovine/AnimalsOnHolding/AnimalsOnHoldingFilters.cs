using Cads.Cds.Api.Core.Domain.Animals;
using Microsoft.AspNetCore.Mvc;

namespace Cads.Cds.Api.Application.Queries.Bovine.AnimalsOnHolding;

public sealed class AnimalsOnHoldingFilters
{
    [FromQuery(Name = "status")]
    public AnimalStatus[]? Status { get; init; }

    [FromQuery(Name = "sex")]
    public AnimalSex? Sex { get; init; }

    [FromQuery(Name = "breedCode")]
    public string[]? BreedCode { get; init; }

    [FromQuery(Name = "dateOnCPHFrom")]
    public DateOnly? DateOnCphFrom { get; init; }

    [FromQuery(Name = "q")]
    public string? Query { get; init; }
}