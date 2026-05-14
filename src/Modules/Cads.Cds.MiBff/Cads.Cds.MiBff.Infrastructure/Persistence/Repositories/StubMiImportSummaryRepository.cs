using Cads.Cds.MiBff.Core.Domain.Entities;
using Cads.Cds.MiBff.Core.Domain.Repositories;

namespace Cads.Cds.MiBff.Infrastructure.Persistence.Repositories;

public class StubMiImportSummaryRepository : IMiImportSummaryRepository
{
    public Task<IEnumerable<MiImportSummary>> GetImportSummaryAsync(DateOnly fromDate, DateOnly toDate, CancellationToken cancellationToken = default)
    {
        var result = Enumerable.Range(1, 25)
            .Select(x => new MiImportSummary()
            {
                CountryMovedFrom = "IRELAND",
                MonthYear = new DateTime(fromDate.Year, fromDate.Month, 1, 0, 0, 0, DateTimeKind.Utc),
                AgeAtMovement = x * 2,
                AgeBand = $"{(x * 2 / 12) * 12} to {(x * 2 / 12 + 1) * 12}",
                BreedType = "Dairy",
                Sex = "F",
                NumberOfMovements = 30 - x
            });

        return Task.FromResult(result);
    }
}