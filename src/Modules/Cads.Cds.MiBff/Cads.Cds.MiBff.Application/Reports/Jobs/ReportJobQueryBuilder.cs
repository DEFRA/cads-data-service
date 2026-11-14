using Cads.Cds.BuildingBlocks.Core.Exceptions;
using Cads.Cds.MiBff.Application.Queries.Reports;
using Cads.Cds.MiBff.Application.Reports.Commands;
using Cads.Cds.MiBff.Application.Reports.Requests;
using Cads.Cds.MiBff.Application.Reports.Routing.Abstractions;
using Cads.Cds.MiBff.Core.Domain.Entities;
using System.Text.Json;

namespace Cads.Cds.MiBff.Application.Reports.Jobs;

public class ReportJobQueryBuilder(IReportRegistry reportRegistry, IServiceProvider serviceProvider)
    : IReportJobQueryBuilder
{
    /// <summary>
    /// Report keys that may be generated asynchronously. Extend as more reports move to the job flow.
    /// </summary>
    internal static readonly IReadOnlySet<string> SupportedReportKeys =
        new HashSet<string>(StringComparer.Ordinal) { GetGbCattleDeathsQuery.ExpectedKey };

    public object Build(MiReportJob job)
    {
        ArgumentNullException.ThrowIfNull(job);

        if (!SupportedReportKeys.Contains(job.ReportKey))
            throw new NonRetryableException($"Report key '{job.ReportKey}' is not supported for asynchronous generation.");

        var (handler, requestType) = reportRegistry.Resolve(job.ReportKey, serviceProvider);

        var reportRequest = Deserialise(job, requestType);

        // The job row is the source of truth for which report is run.
        reportRequest.ReportKey = job.ReportKey;

        return handler.BuildUntypedQuery(reportRequest);
    }

    private static GetReportRequest Deserialise(MiReportJob job, Type requestType)
    {
        try
        {
            return (GetReportRequest?)JsonSerializer.Deserialize(job.RequestPayload, requestType, ReportJobSerializerOptions.Default)
                ?? throw new NonRetryableException($"Request payload for report job {job.ReportJobId} was empty.");
        }
        catch (JsonException ex)
        {
            throw new NonRetryableException(
                $"Request payload for report job {job.ReportJobId} could not be deserialised to {requestType.Name}.", ex);
        }
    }
}