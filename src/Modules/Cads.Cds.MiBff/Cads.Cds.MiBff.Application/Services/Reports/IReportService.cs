using Cads.Cds.MiBff.Core.DTOs.Reports;

namespace Cads.Cds.MiBff.Application.Services.Reports;

public interface IReportService
{
    Task<IEnumerable<ReportDto>> GetUserReportsByEmailAsync(string email, CancellationToken cancellationToken = default);
}