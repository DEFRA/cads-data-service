using Cads.Cds.MiBff.Core.DTOs.Reports;

namespace Cads.Cds.MiBff.Core.Services.Reports;

public interface IReportAccessService
{
    Task<IEnumerable<ReportDto>> GetUserReportsAsync(
        string identifier,
        CancellationToken cancellationToken = default);

    Task<bool> HasReportAccessAsync(
        string externalSubject,
        string reportKey,
        CancellationToken cancellationToken = default);

    Task<IEnumerable<string>> GetUserReportPermissionsAsync(
        string externalSubject,
        string reportKey,
        CancellationToken cancellationToken = default);
}