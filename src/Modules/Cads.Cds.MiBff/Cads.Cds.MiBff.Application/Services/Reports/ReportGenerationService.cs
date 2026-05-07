using Cads.Cds.MiBff.Core.Domain.Repositories;

namespace Cads.Cds.MiBff.Application.Services.Reports;

public class ReportGenerationService(
    IOpenXmlReportGenerator reportGenerator,
    IMiBirthSummaryRepository reportRepository) : IReportGenerationService
{

    public async Task<MemoryStream> GetCattleRegistrations(DateOnly dateTimeFrom, DateOnly dateTimeTo, CancellationToken cancellationToken)
    {
        var data = await reportRepository.GetBirthSummaryAsync(dateTimeFrom, dateTimeTo, cancellationToken);
        return reportGenerator.Generate(data.ToList());
    }
}