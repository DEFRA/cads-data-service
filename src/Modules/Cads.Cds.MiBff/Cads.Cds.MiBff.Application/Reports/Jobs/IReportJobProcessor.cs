namespace Cads.Cds.MiBff.Application.Reports.Jobs;

public interface IReportJobProcessor
{
    /// <summary>
    /// Generates the report for a job, stores the encrypted output and records the outcome on the job.
    /// Jobs that are already finalised are skipped, so redelivered messages are harmless.
    /// </summary>
    Task ProcessAsync(Guid reportJobId, CancellationToken cancellationToken = default);
}