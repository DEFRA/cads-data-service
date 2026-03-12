using Cads.Cds.MiBff.Core.DTOs;

namespace Cads.Cds.MiBff.Core.Services;

public interface IReportService
{
    Task<IEnumerable<ReportListingDto>> GetUserReportListAsync(string? queryUserId, CancellationToken cancellationToken);
}