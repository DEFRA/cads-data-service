using Cads.Cds.MiBff.Core.DTOs.Reports;

namespace Cads.Cds.MiBff.Core.Services.Reports;

public interface IReportService
{
    Task<IEnumerable<ReportDto>> GetUserReportsAsync(string identifier, CancellationToken cancellationToken = default);

}