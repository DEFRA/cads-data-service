using Cads.Cds.BuildingBlocks.Infrastructure.Persistence.Repositories;
using Cads.Cds.MiBff.Core.Domain.Entities;
using Cads.Cds.MiBff.Core.Domain.Repositories;
using Cads.Cds.MiBff.Infrastructure.Persistence.Contexts;
using Microsoft.EntityFrameworkCore;

namespace Cads.Cds.MiBff.Infrastructure.Persistence.Repositories;

public class MiBirthSummaryRepository(MiBffReadDbContext dbContext)
    : EFReadOnlyRepository<MiBirthSummary, MiBffReadDbContext>(dbContext), IMiBirthSummaryRepository
{
    protected virtual IQueryable<MiBirthSummary> QueryBirthSummary(DateOnly fromDate, DateOnly toDate)
        => DbContext.GetBirthsSummary(fromDate, toDate);

    public async Task<IEnumerable<MiBirthSummary>> GetBirthSummaryAsync(DateOnly fromDate, DateOnly toDate, CancellationToken cancellationToken = default)
    {
        return await QueryBirthSummary(fromDate, toDate)
            .AsNoTracking()
            .ToListAsync(cancellationToken);
    }
}

public class StubMiDeathSummaryRepository : IMiDeathSummaryRepository
{
    public Task<IEnumerable<MiDeathSummary>> GetDeathSummaryAsync(DateOnly fromDate, DateOnly toDate, CancellationToken cancellationToken = default)
    {
        var rand = new Random();
        return Task.FromResult(Enumerable.Range(1, 25).Select(x =>
            new MiDeathSummary()
            {
                AgeAtDeathInMonths = rand.Next(1, 24),
                Breed = "Aberdeen Angus",
                BreedCode = "AA",
                BreedType = "NonDairy",
                MonthName = "January",
                Sex = "F",
                Country = "England",
                PremiseTypeGroups = "Farm",
                NumberOfDeaths = rand.Next(1, 100)
            }
        ));
    }
}