using Cads.Cds.MiBff.Application.Reports.Authorisation;
using Cads.Cds.MiBff.Application.Reports.Mappings;
using Cads.Cds.MiBff.Core.Domain.Repositories;
using Cads.Cds.MiBff.Core.DTOs.Reports;

namespace Cads.Cds.MiBff.Infrastructure.Authorisation.Reports;

public class ReportAccessService(
    IMiEffectiveReportPermissionRepository effectiveReportPermissionRepository,
    IMiEffectiveReportAllPermissionRepository effectiveReportAllPermissionRepository) : IReportAccessService
{
    public async Task<IEnumerable<ReportDto>> GetUserReportsAsync(
        string identifier,
        CancellationToken cancellationToken = default)
    {
        var reports = await effectiveReportPermissionRepository.GetActiveByExternalSubjectAsync(identifier, cancellationToken);

        return reports.Select(ReportDtoMapper.Map);
    }

    public async Task<bool> HasReportAccessAsync(
        string externalSubject,
        string reportKey,
        CancellationToken cancellationToken = default)
    {
        return await effectiveReportPermissionRepository.HasReportAccessAsync(externalSubject, reportKey, cancellationToken);
    }

    public async Task<IEnumerable<string>> GetUserReportPermissionsAsync(
        string externalSubject,
        string reportKey,
        CancellationToken cancellationToken = default)
    {
        var permissions = await effectiveReportAllPermissionRepository.GetUserReportPermissionsAsync(externalSubject, reportKey, cancellationToken);

        return [.. permissions.Select(x => x.PermissionKey)];
    }
}