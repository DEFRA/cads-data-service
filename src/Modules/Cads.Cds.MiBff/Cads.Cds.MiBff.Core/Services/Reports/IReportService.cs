using Cads.Cds.MiBff.Core.Domain.DTOs.Reports;

namespace Cads.Cds.MiBff.Core.Services.Reports;

public interface IReportService
{
    Task<IEnumerable<ReportDto>> GetUserReportsAsync(string userId, CancellationToken cancellationToken);
}