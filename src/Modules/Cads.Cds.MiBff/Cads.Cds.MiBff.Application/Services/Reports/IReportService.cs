using Cads.Cds.MiBff.Core.DTOs.Reports;

namespace Cads.Cds.MiBff.Infrastructure.Reports;

public interface IReportService
{
    Task<IEnumerable<ReportDto>> GetUserReportsByEmailAsync(string email, CancellationToken cancellationToken = default);

}