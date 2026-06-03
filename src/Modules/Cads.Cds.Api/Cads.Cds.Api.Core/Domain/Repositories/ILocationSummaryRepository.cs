using Cads.Cds.Api.Core.Domain.Entities;

namespace Cads.Cds.Api.Core.Domain.Repositories;

public interface ILocationSummaryRepository
{
    Task<IEnumerable<LocationSummary>> GetLocationSummaryAsync(string? cph, DateOnly? lastModifiedDate, CancellationToken cancellationToken = default);
}