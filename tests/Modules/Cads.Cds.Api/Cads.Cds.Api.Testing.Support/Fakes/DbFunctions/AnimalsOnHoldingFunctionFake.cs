using Cads.Cds.Api.Core.Domain.Entities.Animals;

namespace Cads.Cds.Api.Testing.Support.Fakes.DbFunctions;

internal static class AnimalsOnHoldingFunctionFake
{
    public static IQueryable<AnimalOnHolding> ApplyGetAnimalsOnHolding(
        this IQueryable<AnimalOnHolding> source,
        string cph,
        long rowFrom,
        long rowTo,
        string? sortField,
        string? sortDirection,
        string? breedCode,
        string? sex)
    {
        var filtered = source.Where(x => x.CphNumber == cph);

        if (!string.IsNullOrWhiteSpace(breedCode))
        {
            var breed = breedCode.Trim();
            filtered = filtered.Where(x => x.BreedIdentifier == breed);
        }

        if (!string.IsNullOrWhiteSpace(sex))
        {
            var sexCode = sex.Trim().ToUpper();
            filtered = filtered.Where(x => x.Sex != null && x.Sex.ToUpper() == sexCode);
        }

        var total = filtered.LongCount();
        var desc = string.Equals(sortDirection, "desc", StringComparison.OrdinalIgnoreCase);

        var ordered = sortField?.ToLowerInvariant() switch
        {
            "date_of_birth" => desc ? filtered.OrderByDescending(x => x.DateOfBirth) : filtered.OrderBy(x => x.DateOfBirth),
            "breed_code" => desc ? filtered.OrderByDescending(x => x.BreedIdentifier) : filtered.OrderBy(x => x.BreedIdentifier),
            "ear_tag_number" => desc ? filtered.OrderByDescending(x => x.EarTagNumber) : filtered.OrderBy(x => x.EarTagNumber),
            "sex" => desc ? filtered.OrderByDescending(x => x.Sex) : filtered.OrderBy(x => x.Sex),
            _ => desc ? filtered.OrderByDescending(x => x.DateOnCph) : filtered.OrderBy(x => x.DateOnCph)
        };

        ordered = ordered
            .ThenBy(x => x.DateOnCph)
            .ThenBy(x => x.EarTagNumber)
            .ThenBy(x => x.AnimalId);

        var from = Math.Max(rowFrom, 1);
        var to = Math.Max(rowTo, from);

        return ordered
            .Skip((int)(from - 1))
            .Take((int)(to - from + 1))
            .Select(x => new AnimalOnHolding
            {
                CphNumber = x.CphNumber,
                AnimalId = x.AnimalId,
                EarTagNumber = x.EarTagNumber,
                EarTagUrlIdentifier = x.EarTagUrlIdentifier,
                DateOfBirth = x.DateOfBirth,
                DateRegistered = x.DateRegistered,
                Sex = x.Sex,
                BreedCode = x.BreedCode,
                Breed = x.Breed,
                AnimalStatus = x.AnimalStatus,
                ResourceType = x.ResourceType,
                CphSchema = x.CphSchema,
                LocationName = x.LocationName,
                IdentifierSchema = x.IdentifierSchema,
                DateOnCph = x.DateOnCph,
                DateOffCph = x.DateOffCph,
                Species = x.Species,
                BreedSchema = x.BreedSchema,
                BreedName = x.BreedName,
                BreedIdentifier = x.BreedIdentifier,
                TotalCount = total
            });
    }
}