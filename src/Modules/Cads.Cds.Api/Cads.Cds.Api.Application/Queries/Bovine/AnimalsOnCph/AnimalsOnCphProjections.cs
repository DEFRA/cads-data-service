using Cads.Cds.Api.Core.Domain.Bovine;
using Cads.Cds.Api.Core.DTOs.Bovine;
using System.Linq.Expressions;

namespace Cads.Cds.Api.Application.Queries.Bovine.AnimalsOnCph;

public static class AnimalsOnCphProjections
{
    public static Expression<Func<AnimalOnCphDto, AnimalSummaryDto>> ToSummary(HoldingAssociation association)
        => association == HoldingAssociation.RegisteredOnHolding ? RegisteredOnHolding : MovedOnHolding;

    private static readonly Expression<Func<AnimalOnCphDto, AnimalSummaryDto>> MovedOnHolding =
        animal => new AnimalSummaryDto
        {
            Identifier = animal.Identifier,
            BirthDate = animal.BirthDate,
            DateOnCph = animal.MovedOnCph,
            DateOffCph = animal.DateOffCph,
            Species = animal.Species,
            Sex = animal.Sex,
            BreedCode = animal.BreedCode,
            Status = animal.Status
        };

    private static readonly Expression<Func<AnimalOnCphDto, AnimalSummaryDto>> RegisteredOnHolding =
        animal => new AnimalSummaryDto
        {
            Identifier = animal.Identifier,
            BirthDate = animal.BirthDate,
            DateOnCph = animal.RegisteredOnCph,
            DateOffCph = animal.DateOffCph,
            Species = animal.Species,
            Sex = animal.Sex,
            BreedCode = animal.BreedCode,
            Status = animal.Status
        };
}