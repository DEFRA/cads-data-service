using Cads.Cds.BuildingBlocks.Application.Queries;
using Cads.Cds.BuildingBlocks.Core.OpenXml;
using Cads.Cds.MiBff.Core.Domain.Repositories;
using Cads.Cds.MiBff.Core.DTOs.Reports;

namespace Cads.Cds.MiBff.Application.Queries.Reports;

public class GetGbCattleRegistrationsQueryHandler(
    IMiBirthSummaryRepository birthSummaryRepository,
    IOpenXmlReportGenerator reportGenerator)
    : IQueryHandler<GetGbCattleRegistrationsQuery, FileReportResult>
{
    public async Task<FileReportResult> Handle(GetGbCattleRegistrationsQuery query, CancellationToken cancellationToken)
    {
        var fromDate = new DateOnly(query.Year, query.Month, 1);
        var toDate = fromDate.AddMonths(1);

        var data = await birthSummaryRepository.GetBirthSummaryAsync(fromDate, toDate, cancellationToken);

        var dataStream = reportGenerator.Generate(data.ToList());

        return new FileReportResult(
            dataStream,
            $"{query.ReportKey}_{DateTime.UtcNow:yyyyMMdd_HHmmss}.xlsx",
            "application/vnd.openxmlformats-officedocument.spreadsheetml.sheet"
        );
    }
}