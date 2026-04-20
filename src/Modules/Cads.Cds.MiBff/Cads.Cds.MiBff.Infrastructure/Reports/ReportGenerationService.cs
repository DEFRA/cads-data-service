using Cads.Cds.MiBff.Application.Services.Reports;
using Cads.Cds.MiBff.Core.Services.Reports;

namespace Cads.Cds.MiBff.Infrastructure.Reports;

public class ReportGenerationService(
    IXlsxReportGenerator reportGenerator,
    IReportRepository reportRepository) : IReportGenerationService
{

    public async Task<MemoryStream> GetCattleMovements()
    {
        var data = await reportRepository.GetCattleMovementReport();
        return reportGenerator.Generate(data);
    }
}