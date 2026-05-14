using Cads.Cds.BuildingBlocks.Application.Queries;
using Cads.Cds.BuildingBlocks.Core.OpenXml;
using Cads.Cds.MiBff.Core.Domain.Repositories;
using Cads.Cds.MiBff.Core.DTOs.Reports;

namespace Cads.Cds.MiBff.Application.Queries.Reports;

public class GetGbCattleImportsQueryHandler(
    IMiImportSummaryRepository importSummaryRepository,
    IOpenXmlReportGenerator reportGenerator)
    : IQueryHandler<GetGbCattleImportsQuery, FileReportResult>
{
    public async Task<FileReportResult> Handle(GetGbCattleImportsQuery query, CancellationToken cancellationToken)
    {
        var fromDate = new DateOnly(query.Year, 1, 1);
        var toDate = fromDate.AddYears(1);

        var data = await importSummaryRepository.GetImportSummaryAsync(fromDate, toDate, cancellationToken);

        var dataStream = reportGenerator.Generate(data.ToList());

        return new FileReportResult(
            dataStream,
            $"{query.ReportKey}_{DateTime.UtcNow:yyyyMMdd_HHmmss}.xlsx",
            "application/vnd.openxmlformats-officedocument.spreadsheetml.sheet"
        );
    }
}