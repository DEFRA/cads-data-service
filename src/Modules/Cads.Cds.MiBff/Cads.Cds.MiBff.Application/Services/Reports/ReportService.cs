using AutoMapper;
using Cads.Cds.MiBff.Core.Domain.Repositories;
using Cads.Cds.MiBff.Core.DTOs.Reports;
using Cads.Cds.MiBff.Core.Services.Reports;

namespace Cads.Cds.MiBff.Application.Services.Reports;

public class ReportService(
    IMiEffectiveReportPermissionRepository effectiveReportPermissionRepository,
    IMapper mapper) : IReportService
{
    public async Task<IEnumerable<ReportDto>> GetUserReportsByEmailAsync(string email, CancellationToken cancellationToken = default)
    {
        var permissions = await effectiveReportPermissionRepository.GetByUserEmailAsync(email, cancellationToken);

        return mapper.Map<IEnumerable<ReportDto>>(permissions);
    }
}