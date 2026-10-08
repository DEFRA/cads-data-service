using Cads.Cds.Api.Application.DTOs.Animals;
using Cads.Cds.Api.Application.DTOs.Bovine.Animals;
using Cads.Cds.Api.Core.Domain.Entities.Animals;

namespace Cads.Cds.Api.Application.Queries.Bovine.AnimalsOnHolding;

public static class AnimalOnHoldingMappingExtensions
{
    public static AnimalSummaryDto ToDto(this AnimalOnHolding s)
    {
        if (s is null) return null!;

        return new AnimalSummaryDto
        {
            Identifier = s.EarTagNumber is not null && s.IdentifierSchema is not null
                ? new AnimalIdentifierDto { Schema = s.IdentifierSchema, Identifier = s.EarTagNumber }
                : null,
            BirthDate = s.DateOfBirth,
            DateOnCph = s.DateOnCph,
            DateOffCph = s.DateOffCph,
            Species = s.Species,
            Sex = ToSexName(s.Sex),
            BreedCode = s.BreedIdentifier is null && s.BreedName is null
                ? null
                : new BreedCodeDto
                {
                    Schema = s.BreedSchema,
                    BreedName = s.BreedName,
                    Identifier = s.BreedIdentifier
                },
            Status = s.AnimalStatus
        };
    }

    public static IReadOnlyList<AnimalSummaryDto> ToDtoList(this IReadOnlyList<AnimalOnHolding> items)
    {
        var list = new List<AnimalSummaryDto>(items.Count);
        foreach (var item in items) list.Add(item.ToDto());
        return list;
    }

    private static string? ToSexName(string? code) => code?.ToUpperInvariant() switch
    {
        "F" => "Female",
        "M" => "Male",
        _ => null
    };
}