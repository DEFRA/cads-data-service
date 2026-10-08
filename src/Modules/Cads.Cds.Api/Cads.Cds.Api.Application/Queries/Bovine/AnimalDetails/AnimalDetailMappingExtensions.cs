using Cads.Cds.Api.Application.DTOs.Animals;
using Cads.Cds.Api.Application.DTOs.Bovine.Animals;
using Cads.Cds.Api.Core.Domain.Entities.Animals;
using System.Globalization;

namespace Cads.Cds.Api.Application.Queries.Bovine.AnimalDetails;

public static class AnimalDetailMappingExtensions
{
    public static AnimalDetailsDto ToDto(this AnimalDetail s) => new()
    {
        ResourceType = s.ResourceType,
        Identifier = s.Identifier,
        EventDateTime = ParseEventDateTime(s.EventDatetime),
        Source = new AnimalDetailSourceDto
        {
            System = s.SourceSystem,
            Schema = s.SourceSchema,
            SchemaVersion = s.SourceSchemaVersion
        },
        AnimalDetail = new AnimalDetailDto
        {
            ResourceType = s.AnimalResourceType,
            Identifier = s.IdentifierSchema is null
                ? null
                : new AnimalIdentifierDto
                {
                    Schema = s.IdentifierSchema,
                    Identifier = s.Identifier
                },
            Species = s.Species,
            Sex = s.Sex,
            BirthDate = s.BirthDate,
            RegistrationDate = s.RegistrationDate,
            DateOnCph = s.DateOnCph,
            BreedCode = s.BreedCode is null
                ? null
                : new BreedCodeDto
                {
                    Schema = s.BreedSchema,
                    BreedName = s.BreedDisplayName ?? s.BreedName,
                    Identifier = s.BreedCode
                },
            Parentage = ToParentage(s),
            State = s.State,
            RestrictionStatus = s.RestrictionStatus
        }
    };

    private static List<ParentageDto> ToParentage(AnimalDetail s)
    {
        var parentage = new List<ParentageDto>(2);

        AddParent(parentage, "GeneticDam", s.GeneticDamIdentifier, s.GeneticDamSchema ?? s.IdentifierSchema);
        AddParent(parentage, "Sire", s.SireIdentifier, s.SireSchema ?? s.IdentifierSchema);

        return parentage;
    }

    private static void AddParent(List<ParentageDto> list, string relationship, string? identifier, string? schema)
    {
        if (string.IsNullOrWhiteSpace(identifier) || string.IsNullOrWhiteSpace(schema)) return;

        list.Add(new ParentageDto
        {
            Relationship = relationship,
            AnimalIdentifier = new AnimalIdentifierDto { Schema = schema, Identifier = identifier }
        });
    }

    /// <summary>
    /// The function returns ISO-8601 UTC text, e.g. 2026-08-24T12:00:00Z
    /// </summary>
    /// <param name="value"></param>
    /// <returns></returns>
    private static DateTime? ParseEventDateTime(string? value) =>
        DateTime.TryParse(value, CultureInfo.InvariantCulture,
            DateTimeStyles.AdjustToUniversal | DateTimeStyles.AssumeUniversal, out var result)
            ? result
            : null;
}