using Cads.Cds.BuildingBlocks.Application.Queries;
using Cads.Cds.BuildingBlocks.Core.Exceptions;
using Cads.Cds.BuildingBlocks.Core.OpenXml;
using Cads.Cds.MiBff.Application.Reports.Jobs;
using Cads.Cds.MiBff.Application.Reports.Routing;
using Cads.Cds.MiBff.Application.Reports.Routing.Abstractions;
using Cads.Cds.MiBff.Application.Reports.Storage;
using Cads.Cds.MiBff.Core.Domain.Entities;
using Cads.Cds.MiBff.Core.Domain.Repositories;
using Cads.Cds.MiBff.Core.DTOs.Reports;
using System.Text.Json;

namespace Cads.Cds.MiBff.Application.Reports.Queries.GetReportJobFile;

public class GetReportJobFileQueryHandler(
    IMiReportJobRepository reportJobRepository,
    IReportJobQueryBuilder queryBuilder,
    IReportFileStore reportFileStore,
    IReportKeyModelRegistry reportKeyModelRegistry,
    IOpenXmlReportGenerator openXmlReportGenerator)
    : IQueryHandler<GetReportJobFileQuery, IReportResult>
{
    private const string XlsxContentType = "application/vnd.openxmlformats-officedocument.spreadsheetml.sheet";

    public async Task<IReportResult> Handle(GetReportJobFileQuery request, CancellationToken cancellationToken)
    {
        var job = await reportJobRepository.GetByReportJobIdAsync(request.JobId, cancellationToken)
            ?? throw new NotFoundException(nameof(MiReportJob), request.JobId);

        if (!string.Equals(job.ReportKey, request.ReportKey, StringComparison.Ordinal))
        {
            throw new NotFoundException(nameof(MiReportJob), request.JobId);
        }

        if (job.Status != ReportJobStatus.Complete)
        {
            throw new NonRetryableException($"Report job {request.JobId} is not complete.");
        }

        var query = queryBuilder.Build(job);
        var storageKey = ReportStorageKeys.For(query.GetType(), job.ReportJobId);

        var decrypted = await reportFileStore.GetAsync(storageKey, cancellationToken);

        if (!reportKeyModelRegistry.TryGetModelType(job.ReportKey, out var modelType))
        {
            throw new NonRetryableException($"Report job {request.JobId} does not have a valid model type.");
        }

        await using (decrypted)
        {
            if (request.FileType == ReportFileType.Json)
            {
                var payload =
                    await JsonSerializer.DeserializeAsync<object>(decrypted, cancellationToken: cancellationToken);
                return new JsonReportResult(payload!);
            }

            var xlsxStream = await openXmlReportGenerator.GenerateFromJsonAsync(
                decrypted, modelType!, cancellationToken: cancellationToken);

            return new FileReportResult(
                xlsxStream,
                $"{job.ReportKey}_{DateTime.UtcNow:yyyyMMdd_HHmmss}.xlsx",
                XlsxContentType);
        }
    }
}