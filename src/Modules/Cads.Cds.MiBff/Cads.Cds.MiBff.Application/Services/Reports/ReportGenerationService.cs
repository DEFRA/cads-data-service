using Cads.Cds.MiBff.Core.Domain.Repositories;

namespace Cads.Cds.MiBff.Application.Services.Reports;

public class ReportGenerationService(
    IOpenXmlReportGenerator reportGenerator,
    IMiReportRepository reportRepository) : IReportGenerationService
{

    public async Task<MemoryStream> GetCattleRegistrations(DateOnly dateTimeFrom, DateOnly dateTimeTo, CancellationToken cancellationToken)
    {
        // todo wire up here
        var data = await reportRepository.GetBirthSummaryAsync(dateTimeFrom, dateTimeTo, cancellationToken);
        return reportGenerator.Generate(data.ToList());
    }
}