using Cads.Cds.Api.Core.Domain.Entities.Holdings;
using Cads.Cds.Api.Core.Domain.Repositories.Holdings;
using Cads.Cds.Api.Infrastructure.Persistence.Contexts;
using Cads.Cds.BuildingBlocks.Infrastructure.Persistence.Repositories;
using Microsoft.EntityFrameworkCore;

namespace Cads.Cds.Api.Infrastructure.Persistence.Repositories.Holdings;

public class LocationSummaryRepository(ApiReadDbContext dbContext)
    : EFReadOnlyRepository<LocationSummary, ApiReadDbContext>(dbContext), ILocationSummaryRepository
{
    protected virtual IQueryable<LocationSummary> QueryLocationSummary(string? cph, DateOnly? lastModifiedDate)
        => DbContext.GetLocationsSummary(cph, lastModifiedDate);

    public async Task<IEnumerable<LocationSummary>> GetLocationSummaryAsync(string? cph, DateOnly? lastModifiedDate, CancellationToken cancellationToken = default)
    {
        return await QueryLocationSummary(cph, lastModifiedDate)
            .AsNoTracking()
            .ToListAsync(cancellationToken);
    }
}