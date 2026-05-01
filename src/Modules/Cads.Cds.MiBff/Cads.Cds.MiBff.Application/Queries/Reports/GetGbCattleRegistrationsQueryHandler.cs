using Cads.Cds.MiBff.Application.Routing.Reports;
using Cads.Cds.MiBff.Application.Routing.Reports.Abstractions;
using Cads.Cds.MiBff.Core.Domain.Repositories;
using Cads.Cds.MiBff.Core.DTOs.Reports;
using Cads.Cds.MiBff.Core.Services.Reports;

namespace Cads.Cds.MiBff.Application.Queries.Reports;

[ReportHandler("gb_cattle_registrations")]
public class GetGbCattleRegistrationsQueryHandler(
    IMiBirthSummaryRepository birthSummaryRepository,
    IXlsxReportGenerator reportGenerator)
    : IReportHandler<GetGbCattleRegistrationsQuery, FileReportResult>
{
    public async Task<FileReportResult> Handle(GetGbCattleRegistrationsQuery query, CancellationToken cancellationToken)
    {
        var fromDate = new DateOnly(query.Year, query.Month, 1);
        var toDate = fromDate.AddMonths(1);

        var data = await birthSummaryRepository.GetBirthSummaryAsync(fromDate, toDate, cancellationToken);
        var dataStream = reportGenerator.Generate();

        return new FileReportResult(
            dataStream,
            $"{query.ReportKey}_{DateTime.UtcNow:yyyyMMdd_HHmmss}.xlsx",
            "application/vnd.openxmlformats-officedocument.spreadsheetml.sheet"
        );
    }
}
