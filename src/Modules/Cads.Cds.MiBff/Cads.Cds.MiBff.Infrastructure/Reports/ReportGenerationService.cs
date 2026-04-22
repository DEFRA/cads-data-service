using Cads.Cds.MiBff.Application.Services.Reports;
using Cads.Cds.MiBff.Core.Services.Reports;

namespace Cads.Cds.MiBff.Infrastructure.Reports;

public class ReportGenerationService(
    IXlsxReportGenerator reportGenerator,
    IReportRepository reportRepository) : IReportGenerationService
{

    public async Task<MemoryStream> GetCattleRegistrations()
    {
        var data = await reportRepository.GetCattleRegistrationReport();
        return reportGenerator.Generate(data);
    }
}