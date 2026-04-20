using AutoMapper;
using Cads.Cds.MiBff.Core.Domain.Repositories;
using Cads.Cds.MiBff.Core.DTOs.Reports;
using Cads.Cds.MiBff.Core.Services.Reports;

namespace Cads.Cds.MiBff.Application.Services.Reports;

public class ReportService(
    IMiEffectiveReportPermissionRepository effectiveReportPermissionRepository,
    IMapper mapper,
    IXlsxReportGenerator reportGenerator,
    IReportRepository reportRepository) : IReportService
{
    public async Task<IEnumerable<ReportDto>> GetUserReportsByEmailAsync(string email, CancellationToken cancellationToken = default)
    {
        var permissions = await effectiveReportPermissionRepository.GetByUserEmailAsync(email, cancellationToken);

        return mapper.Map<IEnumerable<ReportDto>>(permissions);
    }

    public async Task<MemoryStream> GetCattleMovements()
    {
        var data = await reportRepository.GetCattleMovementReport();
        var report = reportGenerator.Generate(data);
        return report;
    }
}

public interface IReportRepository
{
    Task<List<CattleMovement>> GetCattleMovementReport();
}

public class FakeReportRepository : IReportRepository
{
    public Task<List<CattleMovement>> GetCattleMovementReport()
    {
        return Task.FromResult(CattleMovement.GetFakeData(25));
    }
}