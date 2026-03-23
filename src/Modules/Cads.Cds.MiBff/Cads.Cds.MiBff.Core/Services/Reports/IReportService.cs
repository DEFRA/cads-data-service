using Cads.Cds.MiBff.Core.DTOs.Reports;

namespace Cads.Cds.MiBff.Core.Services.Reports;

public interface IReportService
{
    Task<IEnumerable<ReportDto>> GetUserReportsAsync(Guid userId, CancellationToken cancellationToken);

    Task<IEnumerable<ReportDto>> GetUserReportsByEmailAsync(string email, CancellationToken cancellationToken);

}