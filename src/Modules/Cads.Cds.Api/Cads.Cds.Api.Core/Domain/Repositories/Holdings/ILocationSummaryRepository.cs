using Cads.Cds.Api.Core.Domain.Entities.Holdings;

namespace Cads.Cds.Api.Core.Domain.Repositories.Holdings;

public interface ILocationSummaryRepository
{
    Task<IEnumerable<LocationSummary>> GetLocationSummaryAsync(string? cph, DateOnly? lastModifiedDate, CancellationToken cancellationToken = default);
}