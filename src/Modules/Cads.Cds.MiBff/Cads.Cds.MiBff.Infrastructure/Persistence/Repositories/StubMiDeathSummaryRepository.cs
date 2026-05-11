using Cads.Cds.MiBff.Core.Domain.Entities;
using Cads.Cds.MiBff.Core.Domain.Repositories;

namespace Cads.Cds.MiBff.Infrastructure.Persistence.Repositories;

public class StubMiDeathSummaryRepository : IMiDeathSummaryRepository
{
    public Task<IEnumerable<MiDeathSummary>> GetDeathSummaryAsync(DateOnly fromDate, DateOnly toDate, CancellationToken cancellationToken = default)
    {
        return Task.FromResult(Enumerable.Range(1, 25).Select(x =>
            new MiDeathSummary()
            {
                AgeAtDeathInMonths = x,
                Breed = "Aberdeen Angus",
                BreedCode = "AA",
                BreedType = "NonDairy",
                MonthName = "January",
                Sex = "F",
                Country = "England",
                PremiseTypeGroups = "Farm",
                NumberOfDeaths = 37 - x
            }
        ));
    }
}