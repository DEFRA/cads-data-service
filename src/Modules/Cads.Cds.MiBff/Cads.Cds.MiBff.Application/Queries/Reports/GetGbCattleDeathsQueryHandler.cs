using Cads.Cds.BuildingBlocks.Application.Queries;
using Cads.Cds.MiBff.Core.Domain.Repositories;
using Cads.Cds.MiBff.Core.DTOs.Reports;
using System.Text.Json;

namespace Cads.Cds.MiBff.Application.Queries.Reports;

public class GetGbCattleDeathsQueryHandler(IMiDeathSummaryRepository deathSummaryRepository)
    : IQueryHandler<GetGbCattleDeathsQuery, FileReportResult>
{
    public const string JsonContentType = "application/json";

    public async Task<FileReportResult> Handle(GetGbCattleDeathsQuery query, CancellationToken cancellationToken)
    {
        var fromDate = new DateOnly(query.Year, 1, 1);
        var toDate = fromDate.AddYears(1);

        var data = await deathSummaryRepository.GetDeathSummaryAsync(fromDate, toDate, cancellationToken);

        var dataStream = new MemoryStream();
        await JsonSerializer.SerializeAsync(dataStream, data, JsonSerializerOptions.Web, cancellationToken);
        dataStream.Position = 0;

        return new FileReportResult(
            dataStream,
            $"{query.ReportKey}_{DateTime.UtcNow:yyyyMMdd_HHmmss}.json",
            JsonContentType
        );
    }
}