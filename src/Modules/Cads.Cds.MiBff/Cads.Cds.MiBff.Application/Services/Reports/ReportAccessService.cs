using AutoMapper;
using Cads.Cds.MiBff.Core.Domain.Repositories;
using Cads.Cds.MiBff.Core.DTOs.Reports;
using Cads.Cds.MiBff.Core.Services.Reports;

namespace Cads.Cds.MiBff.Application.Services.Reports;

public class ReportAccessService(
    IMiEffectiveReportPermissionRepository effectiveReportPermissionRepository,
    IMiEffectiveReportAllPermissionRepository effectiveReportAllPermissionRepository,
    IMapper mapper) : IReportAccessService
{
    public async Task<IEnumerable<ReportDto>> GetUserReportsAsync(
        string identifier,
        CancellationToken cancellationToken = default)
    {
        var reports = await effectiveReportPermissionRepository.GetActiveByExternalSubjectAsync(identifier, cancellationToken);

        return mapper.Map<IEnumerable<ReportDto>>(reports);
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