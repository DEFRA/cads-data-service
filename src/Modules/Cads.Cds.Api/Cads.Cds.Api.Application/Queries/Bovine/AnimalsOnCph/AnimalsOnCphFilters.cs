using Cads.Cds.Api.Core.Domain.Bovine;
using Cads.Cds.Api.Core.DTOs.Bovine;

namespace Cads.Cds.Api.Application.Queries.Bovine.AnimalsOnCph;

public static class AnimalsOnCphFilters
{
    public static IQueryable<AnimalSummaryDto> Apply(IQueryable<AnimalSummaryDto> animals, GetAnimalsOnCph query)
    {
        animals = animals.Where(a => a.DateOnCph != null);

        var statuses = query.Status.ToList();

        if (statuses.Count > 0)
        {
            animals = animals.Where(a => a.Status != null && statuses.Contains(a.Status.Value));
        }

        if (query.Sex is not null)
        {
            var sex = query.Sex.Value;

            animals = animals.Where(a => a.Sex == sex);
        }

        var breedCodes = query.BreedCode.Select(b => b.ToLower()).ToList();

        if (breedCodes.Count > 0)
        {
            animals = animals.Where(a =>
                a.BreedCode != null
                && a.BreedCode.Identifier != null
                && breedCodes.Contains(a.BreedCode.Identifier.ToLower()));
        }

        if (query.DateOnCphFrom is not null)
        {
            var dateOnCphFrom = query.DateOnCphFrom.Value;

            animals = animals.Where(a => a.DateOnCph >= dateOnCphFrom);
        }

        if (!string.IsNullOrWhiteSpace(query.Q))
        {
            var term = query.Q.ToLower();

            var sex = Enum.TryParse<AnimalSex>(term, ignoreCase: true, out var parsedSex)
                ? parsedSex
                : (AnimalSex?)null;

            animals = animals.Where(a =>
                (a.Identifier != null && a.Identifier.Identifier != null && a.Identifier.Identifier.ToLower().Contains(term))
                || (sex != null && a.Sex == sex)
                || (a.BreedCode != null && a.BreedCode.Identifier != null && a.BreedCode.Identifier.ToLower() == term)
                || (a.BreedCode != null && a.BreedCode.BreedName != null && a.BreedCode.BreedName.ToLower().Contains(term)));
        }

        return animals;
    }
}