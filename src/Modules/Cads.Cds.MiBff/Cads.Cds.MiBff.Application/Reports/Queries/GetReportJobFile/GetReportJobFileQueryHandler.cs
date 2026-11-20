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
    IOpenXmlReportGenerator openXmlReportGenerator,
    ICcoeReportPublisher ccoeReportPublisher)
    : IQueryHandler<GetReportJobFileQuery, IReportResult>
{
    public const int JsonPageSize = 500;

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
                return await BuildPagedJsonResultAsync(decrypted, request.Page, cancellationToken);
            }

            var xlsxStream = await openXmlReportGenerator.GenerateFromJsonAsync(
                decrypted, modelType!, cancellationToken: cancellationToken);

            var downloadLink = await ccoeReportPublisher.PublishXlsxAsync(
                job.ReportKey, job.ReportJobId, xlsxStream, cancellationToken);

            return new RedirectReportResult(downloadLink.Url, downloadLink.ExpiresAt);
        }
    }

    private static async Task<JsonReportResult> BuildPagedJsonResultAsync(
        Stream decrypted, int requestedPage, CancellationToken cancellationToken)
    {
        var page = Math.Max(1, requestedPage);
        var skip = (long)(page - 1) * JsonPageSize;

        var items = new List<JsonElement>(JsonPageSize);
        var hasMore = false;
        long index = 0;

        await foreach (var element in JsonSerializer.DeserializeAsyncEnumerable<JsonElement>(
            decrypted, JsonSerializerOptions.Web, cancellationToken))
        {
            if (index >= skip && items.Count < JsonPageSize)
            {
                // Clone: the element is only valid for the duration of this iteration.
                items.Add(element.Clone());
            }
            else if (index >= skip + JsonPageSize)
            {
                hasMore = true;
                break;
            }

            index++;
        }

        return new JsonReportResult(new PagedReportPayload
        {
            Page = page,
            PageSize = JsonPageSize,
            HasMore = hasMore,
            Items = items
        });
    }
}