using Cads.Cds.MiBff.Core.Domain.Entities;

namespace Cads.Cds.MiBff.Application.Reports.Jobs;

public interface IReportJobQueryBuilder
{
    /// <summary>
    /// Rebuilds the report query for a job from its stored request payload.
    /// Throws <see cref="Cads.Cds.BuildingBlocks.Core.Exceptions.NonRetryableException"/> when the
    /// report is not supported asynchronously or the payload cannot be read.
    /// </summary>
    object Build(MiReportJob job);
}
