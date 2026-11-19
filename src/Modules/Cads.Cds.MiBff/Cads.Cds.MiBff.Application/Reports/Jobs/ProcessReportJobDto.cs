using Cads.Cds.BuildingBlocks.Core.DTOs;

namespace Cads.Cds.MiBff.Application.Reports.Jobs;

public sealed class ProcessReportJobDto : IQueueJob
{
    public Guid? JobId { get; set; }

    public string? CorrelationId { get; set; }

    public required Guid ReportJobId { get; init; }
}