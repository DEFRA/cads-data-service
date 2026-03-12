using Cads.Cds.MiBff.Core.DTOs;

namespace Cads.Cds.MiBff.Core.Services;

public interface IDashboardService
{
    Task<IEnumerable<DashboardListingDto>> GetAllForUserAsync(string queryUserId, CancellationToken cancellationToken);
}